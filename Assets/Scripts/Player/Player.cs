using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Collider2D collider;
    private Animator anim;

    public bool left = false;
    public bool right = false;
    public bool attack = false;
    public bool jump = false;
    public bool isGround = true;
    public bool dash = false;

    private float moveSpeed = 5f;
    private float jumpPower = 10f;



    void Start()
    {
        GameManager.Instance.player = gameObject;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        Move();
        Jump();
        Dash();
        Attack();
    }

    public void Move()
    {
        float x = (right && left) ? 0 : (right) ? 1 : (left) ? -1 : 0;

        rigid.velocity = new Vector2(x * moveSpeed, rigid.velocity.y);
    }
    public void Jump()
    {
        if(jump && isGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGround = false;
        }

        jump = false;
    }
    public void Dash()
    {
        if(dash)
        {
            isGround = true;
        }

        dash = false;
    }
    public void Attack()
    {
        if(attack)
        {

        }
        attack = false;
    }

}
