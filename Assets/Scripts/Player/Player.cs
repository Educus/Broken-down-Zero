using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHitable
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;

    private float maxHp;
    public float playerMaxHp { get { return maxHp; } }
    private float hp;
    public float playerHp { get { return hp; } }
    private float defence;
    private float avoidance;

    [SerializeField] private float moveSpeed; // 기본 이동 속도
    [SerializeField] private float dashRange; // 대쉬 초기 속도
    [SerializeField] private float dashDuration = 0.2f; // 대쉬 지속 시간
    [SerializeField] private float dashCooldown = 1f; // 대쉬 쿨타임


    [HideInInspector]public bool down = false;
    private float jumpPower;
    [SerializeField] private GameObject AttackZone;
    private float power;
    public float mPower { get { return power; } }
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
    private float playerGravity;

    private bool isGround = true;

    private Vector2 dashDirection; // 대쉬 방향 저장
    private float attackTime = 0f; // 공격 시간이 남았는지 체크

    private bool isPlaying = true;


    void Start()
    {
        GameManager.Instance.player = gameObject;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sprite.sortingOrder = 10;
        playerGravity = rigid.gravityScale;

        GetStat();
    }

    void Update()
    {
        IdleCilp();
        anim.SetFloat("Hp", playerHp);

        if (!isPlaying) return;

        Move();

        // 공격 중일 때 이동을 멈추고 타이머만 진행
        // if (isAttacking)
        // {
        //     attackTime -= Time.deltaTime;
        //     if (attackTime <= 0f)
        //     {
        //         EndAttack();
        //     }
        //     return; // 공격 중에는 이동, 대쉬, 점프가 불가능하므로 나머지 처리 중단
        // }

        // 대쉬 쿨타임 처리
        if (dashCooldownTime > 0f)
        {
            dashCooldownTime -= Time.deltaTime;
        }
    }
    private void IdleCilp()
    {
        if(GamePlayerDataManager.Instance.playerWeapon == null)
        {
            anim.runtimeAnimatorController = GamePlayerDataManager.Instance.playerAnim[0];
        }
        else
        {
            anim.runtimeAnimatorController = GamePlayerDataManager.Instance.playerAnim[1];
        }
    }

    void FixedUpdate()
    {
        if (!isPlaying) return;

        // 바닥에 닿았는지 체크 (Ground 또는 Platform 레이어에 닿았을 때만 점프 가능)
        IsGround();
        anim.SetFloat("VelocityY", rigid.velocity.y);

        if (isDashing)
        {
            // 대쉬 중에도 이동의 영향을 받게 하려면 이동 방향을 계속 반영
            // 대쉬 중에는 대쉬 방향으로 날아가면서도 이동 방향을 반영

            // 대쉬 진행 중, 이동 방향을 반영하여 속도를 수정 (대쉬 1)
            // rigid.velocity = new Vector2(dashDirection.x * dashRange + moveDirectionX * moveSpeed, rigid.velocity.y);

            dashTime -= Time.deltaTime;

            // 대쉬가 끝나면 대쉬 종료
            if (dashTime <= 0f)
            {
                StopDash();
            }
        }
    }
    private void GetStat()
    {
        maxHp = DBPlayer.Instance.maxHp;
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
            isflyAttack = true;
        }
        else
        {
            isGround = false;
            anim.SetBool("IsGround", false);
        }
    }

    private void Move()
    {
        if (!isPlaying) return;

        // 대쉬, 공격중일 때 움직임 금지
        if (isDashing) return;

        if (isAttacking)
        {
            rigid.velocity = new Vector2(sprite.flipX ? -0.15f : 0.15f , 0);
        }
        else
        {
            moveDirectionX = (right && left) ? 0 : (right) ? 1 : (left) ? -1 : 0;

            rigid.velocity = new Vector2(moveDirectionX * moveSpeed, rigid.velocity.y);

            anim.SetInteger("Move", (int)rigid.velocity.x);

            float attackZoneX = Mathf.Abs(AttackZone.transform.localPosition.x);

            if (moveDirectionX < 0)
            {
                sprite.flipX = true;
                AttackZone.transform.localPosition = new Vector3(-attackZoneX, AttackZone.transform.localPosition.y);
            }
            else if (moveDirectionX > 0)
            {
                sprite.flipX = false;
                AttackZone.transform.localPosition = new Vector3(attackZoneX, AttackZone.transform.localPosition.y);
            }
        }
    }

    public void Dash()
    {
        if (!isPlaying) return;

        // 대쉬쿨타임, 대쉬중, 공격중일 때 대쉬 금지
        if (dashCooldownTime > 0f || isDashing || isAttacking)
            return;

        isDashing = true;
        dashTime = dashDuration;
        dashCooldownTime = dashCooldown; // 대쉬 후 쿨타임 시작
        StartCoroutine(GameUIManager.Instance.DashCoolTime(dashCooldown));

        rigid.gravityScale = 0;

        rigid.velocity = new Vector2(sprite.flipX ? -1 : 1, 0).normalized * dashRange;

        /*
        // 대쉬 1
        // 대쉬는 버튼을 눌렀을 때 바라보는 방향으로 날아간다
        dashDirection = new Vector2(sprite.flipX ? -1 : 1, 0).normalized;

        // 대쉬 시작 시 AddForce 대신, 직접 속도 설정
        rigid.velocity = new Vector2(dashDirection.x * dashRange, rigid.velocity.y); // 수평 속도만 지정
        */
    }

    void StopDash()
    {
        isDashing = false;
        rigid.gravityScale = playerGravity;
    }

    public void Jump()
    {
        if (!isPlaying) return;

        if (!isGround || isAttacking || isDashing) return;

        if (down)
        {
            JumpingDown();
            return;
        }

        anim.SetTrigger("Jump");
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

    private bool isflyAttack = true;
    // 공격 시작
    public void Attack()
    {
        if (!isPlaying) return;

        // 무기 미장착시 공격 금지
        if (InventoryManager.Instance.inven.getWeapon.dbItem == null) return;

        // 대쉬, 공격중일 때 공격 금지
        if (isDashing || isAttacking) return;

        if (!isGround)
        {
            if (isflyAttack)
            {
                isflyAttack = false;
            }
            else return;
        }

        isAttacking = true;
        attackTime = attackDuration;
        
        // 공격 애니메이션 또는 이펙트 등을 여기서 실행 (예: 애니메이션 실행, 공격 콜백 등)
        anim.SetTrigger("Attack_A");
        rigid.gravityScale = 0;
    }

    // 공격 피해
    public void AttackDamage()
    {
        AttackZone.SetActive(true);
    }

    // 공격 종료
    public void EndAttack()
    {
        isAttacking = false;
        AttackZone.SetActive(false);
        rigid.gravityScale = playerGravity;
    }
    public void Skill1()
    {
        if (!isPlaying) return;

    }
    public void Skill2()
    {
        if (!isPlaying) return;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(0.7f, 0.035f));
        // Gizmos.DrawWireCube(transform.position + new Vector3(0,sprite.bounds.extents.y), new Vector2(0.5f, 1.8f));
    }

    public void IDamage(float damage)
    {
        if (!isPlaying) return;

        hp -= damage;

        if (hp > 0)
            anim.SetTrigger("Hit");
        else
            Dead();

        EndAttack();
    }

    public void Dead()
    {
        isPlaying = false;
        StopDash();
        anim.SetTrigger("Dead");
        rigid.velocity = new Vector2(0, 0);

        if (GameManager.Instance.tutorial)
        {
            StartCoroutine(GameManager.Instance.Recovery(3));
        }
        else if (SceneController.Instance.NowScene() == 2) // 튜토리얼 보스에게 죽었을 때 이벤트
        {
            // 빈칸
        }
        else // 튜토리얼 이외에서 죽었을 때 이벤트
        {

        }
    }
    public void Recovery()
    {
        if (hp > 0) return;

        hp = 100;
        isPlaying = true;
        anim.SetTrigger("Respawn");
    }
}
