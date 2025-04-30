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

    [SerializeField] private GameObject hitZone;    // Ÿ�� ����
    [SerializeField] private GameObject searchZone; // ���� ����
    [SerializeField] private GameObject attackZone; // ���ݽ��� ����

    [SerializeField] private Vector3[] moveVector;
    private int nextMove = 0;

    [Tooltip("�̵� �ݺ� �ֱ�(n�ʸ��� �ݺ�)")]
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

        originPosition = transform.position;    // ���� ��ġ
        maxHp = dbEnemy.EnemyHp;                // �����ͺ��̽����� �ִ� ü�� ��������
        hp = maxHp;                             // ���� ü��
        hpBar.value = (float)hp / maxHp;        // ü�¹� ����
        attackPower = dbEnemy.EnemyATK;         // �����ͺ��̽����� ���ݷ� ��������
        hpBar.gameObject.SetActive(false);      // ü�¹� ��Ȱ��ȭ(�ǰݽ� Ȱ��ȭ)
        speed = dbEnemy.EnemySPEED;

        transform.parent = GameObject.Find("EnemyManagement")?.transform;   // EnemyManagement�� ������ �ű⿡ ��� Enemy ������
    }


    protected virtual void Update()
    {
        if (!isPlaying) return;
        ActionRoutine();    // �÷��̾� ���� Ȯ��

        anim.SetInteger("Move", (int)rigid.velocity.x);

        if (isTarget)
        {
            // �÷��̾ Ž�� ���� �ȿ� ������ �� �÷��̾ ���� �Ÿ� �̳��� �� ������ ����(���ֳ�����)
            // if������ ����� �����Ÿ� �̳��϶� ���ݱ�� �ƴҶ� ������� ����ϰ� �����
            // �÷��̾��� y���� �ڽ��� Ű �̳���� ���� <- boxCollider2D�� Size
            float distance = Mathf.Abs(target.transform.position.x - transform.position.x);

            if (distance < 1)
            {
                return;
            }
            
            Move(target.transform.position.x - transform.position.x > 0 ? 2f : -2f);


            return;
        }

        // target�� ���� �� �ڵ� �̵�
        // �ڵ��̵��� ������ enemy control�� ��밡��
        // �ڵ��̵��� �ִٸ� ������ �� ���·θ� ������
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
                                            // ������ �Ǿ�����
        searchZone.SetActive(!isTarget);    // �������� ��Ȱ��ȭ
        attackZone.SetActive(isTarget);     // ���ݹ��� Ȱ��ȭ
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
