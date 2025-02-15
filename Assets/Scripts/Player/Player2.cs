using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player2 : MonoBehaviour
{
    // 바꾸기 이전
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
        // if (!GameManager.Instance.isPlaying) return;

        IsGround();
        Move();
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
        int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform");
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

    public bool down = false;
    public void Jump()
    {
        if (!isGround) return;

        if (down)
        {
            JumpingDown();
            return;
        }

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGround = false;
    }
    private void JumpingDown()
    {
        // 하단 점프
        int layerMask = 1 << LayerMask.NameToLayer("Platform");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.7f, 0.035f), 0f, Vector2.zero, 0f, layerMask);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;
    public void Dash()
    {
        rigid.AddForce((sprite.flipX ? Vector2.left : Vector2.right) * dashPower, ForceMode2D.Impulse);

        // 순간이동 방식
        /*
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
        }
        */
    }

    public void Attack()
    {

        // 아직 없음

    }

    public void Skill1()
    {

    }
    public void Skill2()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(0.7f, 0.035f));
        // Gizmos.DrawWireCube(transform.position + new Vector3(0,sprite.bounds.extents.y), new Vector2(0.5f, 1.8f));
    }
}
