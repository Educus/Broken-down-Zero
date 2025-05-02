using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public float damage = 0;
    [HideInInspector] public float speed = 0;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (speed != 0)
        {
            rigid.velocity = new Vector2 (speed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (damage == 0) return;

            collision.GetComponent<Player?>().IDamage(damage);
        }
    }
}
