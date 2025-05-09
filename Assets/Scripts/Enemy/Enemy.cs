using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private DBEnemy dbEnemy;
    [SerializeField] private Slider hpBar;

    protected float maxHp;
    protected float hp;
    protected float attackPower;
    protected float speed;
    public float mPower { get { return attackPower; } }

    [SerializeField] private GameObject hitZone;    // 타격 범위
    [SerializeField] private GameObject searchZone; // 감지 범위
    [SerializeField] private GameObject attackZone; // 공격시작 범위

    [SerializeField] private Vector3[] moveVector;
    private int nextMove = 0;

    [Tooltip("이동 반복 주기(n초마다 반복)")]
    [SerializeField] private float interval;
    private float intervalTime = 0f;
    private bool isMove = false;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    [HideInInspector] public bool isPlaying = true;

    private GameObject target = null;
    private bool isTarget = false;
    private bool isAttack = false;
    private bool isDamage = false;

    Vector3 originPosition;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        originPosition = transform.position;    // 원래 위치
        maxHp = dbEnemy.EnemyHp;                // 데이터베이스에서 최대 체력 가져오기
        hp = maxHp;                             // 현재 체력
        hpBar.value = (float)hp / maxHp;        // 체력바 비율
        attackPower = dbEnemy.EnemyATK;         // 데이터베이스에서 공격력 가져오기
        hpBar.gameObject.SetActive(false);      // 체력바 비활성화(피격시 활성화)
        speed = dbEnemy.EnemySPEED;

        transform.parent = GameObject.Find("EnemyManagement")?.transform;   // EnemyManagement가 있으면 거기에 모든 Enemy 모으기
    }


    protected virtual void Update()
    {
        if (!isPlaying) return;
        ActionRoutine();    // 플레이어 감지 확인

        anim.SetInteger("Move", (int)rigid.velocity.x);

        if (isTarget)
        {
            // 플레이어가 탐지 범위 안에 들어왔을 때 플레이어를 일정 거리 이내가 될 때까지 따라감(우주끝까지)
            // if문으로 대상이 일정거리 이내일때 공격기능 아닐때 추적기능 사용하게 만들기
            // 플레이어의 y축이 자신의 키 이내라면 공격 <- boxCollider2D의 Size
            float distance = Mathf.Abs(target.transform.position.x - transform.position.x);

            if (distance < 1)
            {
                return;
            }
            
            Move(target.transform.position.x - transform.position.x > 0 ? 2f : -2f);


            return;
        }

        // target이 없을 때 자동 이동
        // 자동이동이 없으면 enemy control로 사용가능
        // 자동이동이 있다면 무조건 그 상태로만 움직임
        if (moveVector.Length == 0) return;

        intervalTime += Time.deltaTime;
        if (intervalTime > interval)
        {
            intervalTime = 0;
            isMove = !isMove;
        }

        if (isMove)
        {
            if ((int)(originPosition.x + moveVector[nextMove].x) == (int)transform.position.x)
            {
                if (nextMove < moveVector.Length - 1)
                {
                    nextMove += 1;
                }
                else
                    nextMove = 0;
            }
            else
            {
                Move(originPosition.x + moveVector[nextMove].x - transform.position.x > 0 ? 1 : -1);
            }
        }
        else
        {
            Move(0f);
        }
    }

    private void ActionRoutine()
    {
        bool isTarget = target == null ? false : true;
                                            // 감지가 되었으면
        searchZone.SetActive(!isTarget);    // 감지범위 비활성화
        attackZone.SetActive(isTarget);     // 공격범위 활성화
    }
    public void FindTarget(GameObject getTarget)
    {
        target = getTarget;

        if (target != null)
        {
            isTarget = true;
        }
    }
    public virtual void Move(float value)
    {
        if (!isPlaying) return;
        if (isAttack || isDamage)
        {
            rigid.velocity = new Vector2 (0f, 0f);
            return;
        }

        if (value == 0) return;

        
        if (value < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        int flipX = sprite.flipX ? -1 : 1;

        hitZone.transform.localScale = new Vector3(flipX, 1, 1);
        attackZone.transform.localScale = new Vector3(flipX, 1, 1);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.3f, 0), flipX * transform.right, 1f, LayerMask.GetMask("Ground"));
        if(hit.collider != null) return;

        rigid.velocity = new Vector2(value, rigid.velocity.y);
    }
    public virtual void Attack()
    {
        if (!isPlaying) return;
        if (isAttack) return;
        if (isDamage) return;
        
        rigid.velocity = new Vector2(0f, 0f);
        isAttack = true;
        anim.SetTrigger("Attack");
    }

    public void AttackDamage()
    {
        hitZone.SetActive(true);
    }
    public void EndAttack()
    {
        isAttack = false;
        hitZone.SetActive(false);
    }

    public void IDamage(float damage)
    {
        if (!isPlaying) return;

        rigid.velocity = new Vector2(0f, 0f);
        isDamage = true;
        hp -= damage;

        hpBar.gameObject.SetActive(true);
        hpBar.value = hp / maxHp;

        if (hp > 0)
            anim.SetTrigger("Hit");
        else
            Dead();

        EndAttack();
    }
    public void IEndDamage()
    {
        isDamage = false;
    }
    public void Dead()
    {
        isPlaying = false;
        rigid.velocity = Vector3.zero;
        hpBar.gameObject.SetActive(false);
        anim.SetTrigger("Dead");
        // gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (moveVector == null) return;

        Gizmos.color = Color.red;

        Vector3 pivot = Application.isPlaying ? originPosition : transform.position;

        for (int i = 0; i < moveVector.Length - 1; i++)
        {
            Vector3 start = pivot + moveVector[i];
            Vector3 end = pivot + moveVector[i + 1];
            Gizmos.DrawLine(start, end);
        }

        Gizmos.DrawRay(transform.position + new Vector3(0, 0.3f, 0), transform.right);
    }
}
