using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] public float hp;
    [SerializeField] private float attackPower;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    public bool isPlaying = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        hp = 100;
    }
    void Update()
    {
        if (!isPlaying) return;

        if (hp <= 0)
        {
            Dead();
        }

        anim.SetInteger("Move", (int)rigid.velocity.x);
    }
    public void Move(float value)
    {
        if (!isPlaying) return;

        rigid.velocity = new Vector2(value, 0);

        if (value == 0) return;
        sprite.flipX = value < 0 ? true : false;
    }
    public void Attack()
    {
        if (!isPlaying) return;

        anim.SetTrigger("Attack");
    }

    public void IDamage(float damage)
    {
        if (!isPlaying) return;

        anim.SetTrigger("Hit");
        hp -= damage;
    }
    public void Dead()
    {
        isPlaying = false;
        anim.SetTrigger("Dead");
        // gameObject.SetActive(false);
    }
}
