using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public bool isAlive = true;
    public int health;
    public bool invunerable = false;

    public float maxSpeed;
    public float jumpForce;

    private bool grounded;
    private bool jumping;
    public bool walled;

    private Rigidbody2D rgbd;
    private Animator anima;
    private SpriteRenderer sprite;

    public Transform groundCheck;

    public Transform bulletSpawn;
    public GameObject bulletObject;
    public float fireRate;
    public float nextFire;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        walled = Physics2D.Linecast(transform.position, bulletSpawn.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }

        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Fire();
        }
    }

    public void FixedUpdate()
    {
        if (isAlive)
        {
            float move = Input.GetAxis("Horizontal");

            rgbd.velocity = new Vector2(move * maxSpeed, rgbd.velocity.y);

            if (move > 0 && sprite.flipX || move < 0 && !sprite.flipX)
            {
                if(walled && !grounded)
                {
                    rgbd.velocity = (new Vector2(rgbd.velocity.x, 0f));
                    rgbd.AddForce(new Vector2(0, jumpForce));
                }

                Flip();
            }

            anima.SetFloat("speed", Mathf.Abs(move));

            if (jumping)
            {
                rgbd.AddForce(new Vector2(0, jumpForce));
                jumping = false;
            }

            anima.SetBool("jumpFall", rgbd.velocity.y != 0);

        }
        else
        {
            rgbd.velocity = new Vector2(0, rgbd.velocity.x);
        }

    }

    public void Flip()
    {
        sprite.flipX = !sprite.flipX;

        if(!sprite.flipX)
        {
            bulletSpawn.transform.position = new Vector3(transform.position.x + 0.11f, transform.position.y);
        }
        else
        {
            bulletSpawn.transform.position = new Vector3(transform.position.x - 0.11f, transform.position.y);
        }
    }

    public void Fire()
    {
        anima.SetTrigger("fire");

        nextFire = Time.time + fireRate;

        GameObject cloneBullet = Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);

        if(sprite.flipX)
        {
            cloneBullet.transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

    IEnumerator Damage()
    {
        for(float i=0f; i < 1f; i +=0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invunerable = false;
    }

    public void DamagePlayer()
    {
        if (isAlive)
        {          
            invunerable = true;
            health--;
            StartCoroutine(Damage());
            CameraController.instance.shake();

            if (health < 1)
            {
                anima.SetTrigger("death");
                isAlive = false;
                Invoke("ReloadLevel", 3f);
            }
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
