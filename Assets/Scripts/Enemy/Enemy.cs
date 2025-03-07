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

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    public bool isPlaying = true;

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

        if (moveVector == null) return;

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
