using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    private float timeDestroy;

    public int damage;
	// Use this for initialization
	void Start () {
        timeDestroy = 1.0f;
        Destroy(gameObject, timeDestroy);
        damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.DamageEnemy(damage);
                Destroy(gameObject);
            }
        }
    }
}
