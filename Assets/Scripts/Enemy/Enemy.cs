using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] public float hp;
    [SerializeField] private float attackPower = 1;
    public float mPower { get { return attackPower; } }
    [SerializeField] private GameObject attackZone;

    [SerializeField] private Vector3[] moveVector;
    private int nextMove = 0;
    [SerializeField] private float interval;
    private float intervalTime = 0f;
    private bool isMove = false;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    public bool isPlaying = true;

    private GameObject target = null;
    private bool isTarget = false;

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

        anim.SetInteger("Move", (int)rigid.velocity.x);

        if (isTarget)
        {
            // 플레이어가 탐지 범위 안에 들어왔을 때 플레이어를 일정 거리 이내가 될 때까지 따라감(우주끝까지)
            // 플레이어의 y축이 자신의 키 이내라면 공격 <- boxCollider2D의 Size
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

        rigid.velocity = new Vector2(value, 0);

        if (value == 0) return;

        float attackZoneX = Mathf.Abs(attackZone.transform.localPosition.x);

        if (value < 0)
        {
            sprite.flipX = true;
            attackZone.transform.localPosition = new Vector3(-attackZoneX, attackZone.transform.localPosition.y);
        }
        else
        {
            sprite.flipX = false;
            attackZone.transform.localPosition = new Vector3(attackZoneX, attackZone.transform.localPosition.y);
        }
    }
    public void Attack()
    {
        if (!isPlaying) return;

        anim.SetTrigger("Attack");
    }

    public void AttackDamage()
    {
        attackZone.SetActive(true);
    }
    public void EndAttack()
    {
        attackZone.SetActive(false);
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

    }
}
