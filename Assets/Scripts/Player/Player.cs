using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;


    private float hp;
    private float power;
    private float defence;
    private float moveSpeed;
    private float attackSpeed;
    private float jumpPower;
    private float dashRange;
    private float critical;
    private float criticalDamage;
    private float avoidance;

    void Start()
    {
        GameManager.Instance.player = gameObject;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sprite.sortingOrder = 10;

        GetStat();
    }

    void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        IsGround();
        Move();
        Jump();
        Dash();
        Attack();
        Skill1();
        Skill2();
    }
    
    private void GetStat()
    {
        hp = DBPlayer.Instance.maxHp;
        power = DBPlayer.Instance.power;
        defence = DBPlayer.Instance.defence;
        moveSpeed = DBPlayer.Instance.moveSpeed;
        attackSpeed = DBPlayer.Instance.attackSpeed;
        jumpPower = DBPlayer.Instance.jumpPower;
        dashRange = DBPlayer.Instance.dashRange;
        critical = DBPlayer.Instance.critical;
        criticalDamage = DBPlayer.Instance.criticalDamage;
        avoidance = DBPlayer.Instance.avoidance;
    }

    public bool isGround = true;
    private void IsGround()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.7f, 0.035f), 0f, Vector2.zero, 0f, layerMask);

        if(hit.collider != null)
        {
            isGround = true;
            anim.SetBool("IsGround", true);
        }
        else
        {
            isGround = false;
            anim.SetBool("IsGround", false);
        }
    }

    public bool left = false;
    public bool right = false;
    private void Move()
    {
        float x = (right && left) ? 0 : (right) ? 1 : (left) ? -1 : 0;

        rigid.velocity = new Vector2(x * moveSpeed, rigid.velocity.y);
        anim.SetInteger("Move", (int)rigid.velocity.x);

        if (x < 0)
        {
            sprite.flipX = true;
        }
        else if (x > 0)
        {
            sprite.flipX = false;
        }
    }

    public bool jump = false;
    public bool down = false;
    private void Jump()
    {
        if (!jump) return;
        if (!isGround) return;

        if (down)
        {
            JumpingDown();
            return;
        }

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGround = false;

        jump = false;
    }
    private void JumpingDown()
    {
        // 하단 점프
    }

    public bool dash = false;
    private void Dash()
    {
        if(!dash) return;

        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        int x = (sprite.flipX) ? -1 : 1;

        RaycastHit2D hit = Physics2D.BoxCast(transform.position + new Vector3(0, sprite.bounds.extents.y), new Vector2(0.5f, 1.8f), 0f, transform.right * x, dashRange, layerMask);

        if (hit.collider == null)
        {
            transform.position += new Vector3((sprite.flipX) ? -1 : 1, 0, 0) * dashRange;
        }
        else
        {
            transform.position += new Vector3((sprite.flipX) ? -1 : 1, 0, 0) * hit.distance;
            Debug.Log(hit.distance);
        }

        dash = false;
    }

    public bool attack = false;
    private void Attack()
    {
        if(!attack) return;

        // 아직 없음

        attack = false;
    }

    public bool skill1 = false;
    private void Skill1()
    {
        if (!skill1) return;

        skill1 = false;

    }
    public bool skill2 = false;
    private void Skill2()
    {
        if (!skill2) return;

        skill2 = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(0.7f, 0.035f));
        // Gizmos.DrawWireCube(transform.position + new Vector3(0,sprite.bounds.extents.y), new Vector2(0.5f, 1.8f));
    }
}
