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

    [SerializeField] private float moveSpeed; // �⺻ �̵� �ӵ�
    [SerializeField] private float dashRange; // �뽬 �ʱ� �ӵ�
    [SerializeField] private float dashDuration = 0.2f; // �뽬 ���� �ð�
    [SerializeField] private float dashCooldown = 1f; // �뽬 ��Ÿ��


    [HideInInspector]public bool down = false;
    private float jumpPower;
    [SerializeField] private GameObject AttackZone;
    private float power;
    public float mPower { get { return power; } }
    public float attackDuration = 0.5f; // ���� ���� �ð�
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

    private Vector2 dashDirection; // �뽬 ���� ����
    private float attackTime = 0f; // ���� �ð��� ���Ҵ��� üũ

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

        // ���� ���� �� �̵��� ���߰� Ÿ�̸Ӹ� ����
        // if (isAttacking)
        // {
        //     attackTime -= Time.deltaTime;
        //     if (attackTime <= 0f)
        //     {
        //         EndAttack();
        //     }
        //     return; // ���� �߿��� �̵�, �뽬, ������ �Ұ����ϹǷ� ������ ó�� �ߴ�
        // }

        // �뽬 ��Ÿ�� ó��
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

        // �ٴڿ� ��Ҵ��� üũ (Ground �Ǵ� Platform ���̾ ����� ���� ���� ����)
        IsGround();
        anim.SetFloat("VelocityY", rigid.velocity.y);

        if (isDashing)
        {
            // �뽬 �߿��� �̵��� ������ �ް� �Ϸ��� �̵� ������ ��� �ݿ�
            // �뽬 �߿��� �뽬 �������� ���ư��鼭�� �̵� ������ �ݿ�

            // �뽬 ���� ��, �̵� ������ �ݿ��Ͽ� �ӵ��� ���� (�뽬 1)
            // rigid.velocity = new Vector2(dashDirection.x * dashRange + moveDirectionX * moveSpeed, rigid.velocity.y);

            dashTime -= Time.deltaTime;

            // �뽬�� ������ �뽬 ����
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

        // �뽬, �������� �� ������ ����
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

        // �뽬��Ÿ��, �뽬��, �������� �� �뽬 ����
        if (dashCooldownTime > 0f || isDashing || isAttacking)
            return;

        isDashing = true;
        dashTime = dashDuration;
        dashCooldownTime = dashCooldown; // �뽬 �� ��Ÿ�� ����
        StartCoroutine(GameUIManager.Instance.DashCoolTime(dashCooldown));

        rigid.gravityScale = 0;

        rigid.velocity = new Vector2(sprite.flipX ? -1 : 1, 0).normalized * dashRange;

        /*
        // �뽬 1
        // �뽬�� ��ư�� ������ �� �ٶ󺸴� �������� ���ư���
        dashDirection = new Vector2(sprite.flipX ? -1 : 1, 0).normalized;

        // �뽬 ���� �� AddForce ���, ���� �ӵ� ����
        rigid.velocity = new Vector2(dashDirection.x * dashRange, rigid.velocity.y); // ���� �ӵ��� ����
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
        // �ϴ� ����
        int layerMask = 1 << LayerMask.NameToLayer("Platform");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.7f, 0.035f), 0f, Vector2.zero, 0f, layerMask);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private bool isflyAttack = true;
    // ���� ����
    public void Attack()
    {
        if (!isPlaying) return;

        // ���� �������� ���� ����
        if (InventoryManager.Instance.inven.getWeapon.dbItem == null) return;

        // �뽬, �������� �� ���� ����
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
        
        // ���� �ִϸ��̼� �Ǵ� ����Ʈ ���� ���⼭ ���� (��: �ִϸ��̼� ����, ���� �ݹ� ��)
        anim.SetTrigger("Attack_A");
        rigid.gravityScale = 0;
    }

    // ���� ����
    public void AttackDamage()
    {
        AttackZone.SetActive(true);
    }

    // ���� ����
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
        else if (SceneController.Instance.NowScene() == 2) // Ʃ�丮�� �������� �׾��� �� �̺�Ʈ
        {
            // ��ĭ
        }
        else // Ʃ�丮�� �̿ܿ��� �׾��� �� �̺�Ʈ
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
