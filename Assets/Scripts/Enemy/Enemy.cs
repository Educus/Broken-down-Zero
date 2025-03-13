using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] public float hp;
    [SerializeField] private float attackPower = 1;
    public float mPower { get { return attackPower; } }
    [SerializeField] private GameObject hitZone;
    [SerializeField] private GameObject searchZone;
    [SerializeField] private GameObject attackZone;

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

    Vector3 originPosition;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        originPosition = transform.position;
        hp = 100;
    }
    void Update()
    {
        if (!isPlaying) return;
        ActionRoutine();

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

        searchZone.SetActive(!isTarget);
        attackZone.SetActive(isTarget);
    }
    public void FindTarget(GameObject getTarget)
    {
        target = getTarget;

        if (target != null)
        {
            isTarget = true;
        }
    }
    public void Move(float value)
    {
        if (!isPlaying) return;
        if (isAttack) return;

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

        rigid.velocity = new Vector2(value, 0);
    }
    public void Attack()
    {
        if (!isPlaying) return;
        if (isAttack) return;

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

        hp -= damage;

        if (hp > 0)
            anim.SetTrigger("Hit");
        else
            Dead();
    }
    public void Dead()
    {
        isPlaying = false;
        rigid.velocity = Vector3.zero;
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
