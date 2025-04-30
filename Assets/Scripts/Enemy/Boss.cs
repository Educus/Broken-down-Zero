using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Boss : MonoBehaviour, IHitable
{
    [SerializeField] private DBEnemy dbEnemy;   // 몬스터 정보
    [SerializeField] private Slider hpBar;      // 몬스터 체력바

    protected Player player;  // 플레이어

    protected float bossID;     // 보스ID
    protected float maxHp;      // 최대 체력
    protected float nowHp;      // 현재 체력
    protected float atk;        // 공격력
    protected float atkSpeed;   // 공격속도
    protected float def;        // 방어력
    protected float speed;      // 이동속도

    protected Rigidbody2D rigid;
    protected SpriteRenderer sprite;
    protected Animator anim;

    [HideInInspector]
    public bool isPlaying = false; // 생존유무(외부용)
    [HideInInspector]
    public bool isMove = false; // 이동유무(외부용)
    [HideInInspector]
    public bool isAttack = false; // 공격유무(외부용)

    protected float attackTimer;
    protected bool isAttacking = false; // 공격중(내부용)

    private void Awake()
    {
        player = GameManager.Instance.player?.GetComponent<Player?>();

        bossID = dbEnemy.EnemyID;
        maxHp = dbEnemy.EnemyHp;
        nowHp = maxHp;
        atk = dbEnemy.EnemyATK;
        atkSpeed = dbEnemy.EnemyATKSPEED;
        def = dbEnemy.EnemyDEF;
        speed = dbEnemy.EnemySPEED;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        attackTimer = atkSpeed;
    }

    protected virtual void Update()
    {
        if (isPlaying) // 살아있을 때
        {
            if (!isAttacking) // 공격중이 아닐 때
            {
                Move(speed);
                Attack();
            }

            // 공격 타이머 (atkSpeed마다 공격)
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                attackTimer = atkSpeed; 
                isAttack = true;
            }
        }
        else // 살아있지 않을 때
        {
            Move(0f);
        }
    }

    /// <summary>
    /// isAttacking을 사용하여 공격중이 아닐 때 움직임을 만들고 공격중일때는 움직이지 않게 만들것
    /// </summary>
    protected abstract void Move(float value);

    protected virtual void Attack()
    {
        if (!isAttack) return;  // 공격명령이 안들어왔다면 return;
        if (isAttacking) return; // 공격중이라면 return;

        isAttack = false;
        isAttacking = true;
        Move(0f); // 공격 시작 전 멈추기

        int value = Random.Range(0, 3); // 무작위 패턴 공격 <- 지금은 동일 확률

        switch (value)
        {
            case 0:
                AttackA();
                break;
            case 1:
                AttackB();
                break;
            case 2:
                AttackC();
                break;
            case 3:
                AttackD();
                break;
            default:
                break;
        }
    }

    protected abstract void AttackA();
    protected abstract void AttackB();
    protected abstract void AttackC();
    protected abstract void AttackD();
    public virtual void EndAttack() // isAttacking 종료
    {
        isAttacking = false;
    }
    protected abstract void Dead();

    public void IDamage(float damage)
    {
        if (!isPlaying) return;

        rigid.velocity = new Vector2(0f, 0f);
        nowHp -= damage;

        hpBar.gameObject.SetActive(true);
        hpBar.value = nowHp / maxHp;

        if (nowHp > 0)
            anim.SetTrigger("Hit");
        else
            Dead();

        EndAttack();
    }
}
