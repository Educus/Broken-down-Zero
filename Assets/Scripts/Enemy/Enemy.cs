using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] public float hp;
    [SerializeField] private float attackPower = 1;
    public float mPower { get { return attackPower; } }
    [SerializeField] private GameObject attackZone;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    public bool isPlaying = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        hp = 100;
    }
    void Update()
    {
        if (!isPlaying) return;

        anim.SetInteger("Move", (int)rigid.velocity.x);
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
        anim.SetTrigger("Dead");
        // gameObject.SetActive(false);
    }
}
