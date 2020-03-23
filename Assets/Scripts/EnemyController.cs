using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int health;
    public float distanceAttack;
    public float speed;

    protected bool isMoving = false;

    protected Rigidbody2D rig2d;
    protected Animator anima;
    protected Transform player;
    protected SpriteRenderer sprite;

    private void Awake()
    {
        rig2d = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    protected void Flip()
    {
        sprite.flipX = !sprite.flipX;
        speed *= -1;
    }

    protected virtual void Update()
    {
        float distance = PlayerDistance();

        isMoving = (distance <= distanceAttack);

        if (isMoving)
        {
            if ((player.position.x > transform.position.x && sprite.flipX) ||
                (player.position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
            }
        }
    }

    public void DamageEnemy(int damageBullet)
    {
        health -= damageBullet;

        StartCoroutine(Damage());

        if(health < 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Damage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
