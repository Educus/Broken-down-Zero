using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;

    private float hp;
    private float defence;
    private float avoidance;

    [SerializeField] private float moveSpeed; // 기본 이동 속도
    [SerializeField] private float dashRange; // 대쉬 초기 속도
    [SerializeField] private float dashDuration = 0.2f; // 대쉬 지속 시간
    [SerializeField] private float dashCooldown = 1f; // 대쉬 쿨타임


    [HideInInspector]public bool down = false;
    private float jumpPower;
    private float power;
    public float attackDuration = 0.5f; // 공격 지속 시간
    private float attackSpeed;
    private float critical;
    private float criticalDamage;

    [HideInInspector]public bool left = false;
    [HideInInspector]public bool right = false;
    private float moveDirectionX;
    private bool isDashing = false;
    private bool isAttacking = false;
    private float dashTime = 0f;
    private float dashCooldownTime = 0f;
    private bool isGround = true;

    private Vector2 dashDirection; // 대쉬 방향 저장
    private float attackTime = 0f; // 공격 시간이 남았는지 체크



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
        Move();

        // 공격 중일 때 이동을 멈추고 타이머만 진행
        if (isAttacking)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0f)
            {
                EndAttack();
            }
            return; // 공격 중에는 이동, 대쉬, 점프가 불가능하므로 나머지 처리 중단
        }

        // 대쉬 쿨타임 처리
        if (dashCooldownTime > 0f)
        {
            dashCooldownTime -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // 바닥에 닿았는지 체크 (Ground 또는 Platform 레이어에 닿았을 때만 점프 가능)
        IsGround();

        if (isDashing)
        {
            // 대쉬 중에도 이동의 영향을 받게 하려면 이동 방향을 계속 반영
            // 대쉬 중에는 대쉬 방향으로 날아가면서도 이동 방향을 반영

            // 대쉬 진행 중, 이동 방향을 반영하여 속도를 수정
            rigid.velocity = new Vector2(dashDirection.x * dashRange + moveDirectionX * moveSpeed, rigid.velocity.y);

            dashTime -= Time.deltaTime;

            // 대쉬가 끝나면 대쉬 종료
            if (dashTime <= 0f)
            {
                StopDash();
            }
        }
        else
        {
            // 대쉬 중이 아닐 때는 좌우만 이동
            Move();
        }
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

    private void IsGround()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.7f, 0.035f), 0f, Vector2.zero, 0f, layerMask);

        if (hit.collider != null)
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

    private void Move()
    {
        moveDirectionX = (right && left) ? 0 : (right) ? 1 : (left) ? -1 : 0;

        rigid.velocity = new Vector2(moveDirectionX * moveSpeed, rigid.velocity.y);

        anim.SetInteger("Move", (int)rigid.velocity.x);

        if (moveDirectionX < 0)
        {
            sprite.flipX = true;
        }
        else if (moveDirectionX > 0)
        {
            sprite.flipX = false;
        }
    }

    public void Dash()
    {
        if (dashCooldownTime > 0f || isDashing || isAttacking)
            return;

        isDashing = true;
        dashTime = dashDuration;
        dashCooldownTime = dashCooldown; // 대쉬 후 쿨타임 시작

        // 대쉬는 버튼을 눌렀을 때 바라보는 방향으로 날아간다
        dashDirection = new Vector2(sprite.flipX ? -1 : 1, 0).normalized;

        // 대쉬 시작 시 AddForce 대신, 직접 속도 설정
        rigid.velocity = new Vector2(dashDirection.x * dashRange, rigid.velocity.y); // 수평 속도만 지정
    }

    void StopDash()
    {
        isDashing = false;
    }

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

    // 공격 시작
    public void Attack()
    {
        isAttacking = true;
        attackTime = attackDuration;

        // 공격 애니메이션 또는 이펙트 등을 여기서 실행 (예: 애니메이션 실행, 공격 콜백 등)
        Debug.Log("Attack Started!");

        // 공격 중에 이동, 점프, 대쉬가 불가능하므로 다른 동작을 하지 않도록 막음
        rigid.velocity = Vector2.zero; // 이동 중 공격 시 이동을 멈추게 설정
    }

    // 공격 종료
    void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Attack Ended!");
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
