using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Boss
{
    [SerializeField] GameObject atkA_Hit;
    SpriteRenderer atkA_sprite;

    private Coroutine attackCoroutine;

    private void Start()
    {
        atkA_sprite = atkA_Hit.GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AttackA();
        }
        if(isPlaying) // ������� ��
        {
            if (!isAttacking) // �������� �ƴ� ��
            {
                Move(speed);
                Attack();
            }
        }
        else // ������� ���� ��
        {
            Move(0f);
        }

    }
    protected override void Move(float value)
    {
        if (isMove) // �����̴°�?
        {
            LookPlayer();

        }
        else
        {
            rigid.velocity = new Vector2 (0f, 0f);
        }
    }

    // ���� ����
    // 
    // �����δٰ� ����
    // 
    // �÷��̾�� �����Ÿ� �̻��� �� �÷��̾ ���� �����δ� <- ���鿹��
    // �����ð����� ����� ����Ѵ� <- 0
    // ����� �������� �ʰ� ���ڸ����� ����Ѵ� <- 0

    private void LookPlayer()
    {
        sprite.flipX = transform.position.x - player.transform.position.x >= 0 ? false : true;
    }
        
    protected override void AttackA() // ���� ����
    {
        LookPlayer();
        anim.SetTrigger("AttackA_1");
        atkA_Hit.SetActive(true);
        attackCoroutine = StartCoroutine(IESpriteA(atkA_sprite));
    }
    public void AttackA_2()
    {
        // ����2 ����, ��� ����, �� �ʱ�ȭ �� ��Ȱ��ȭ
        atkA_Hit.GetComponent<BossAttackZone>().HitDamage(atk);
        anim.SetTrigger("AttackA_2");
        StopCoroutine(attackCoroutine);
        atkA_sprite.color = new Color(1, 0, 0, 0f);
        atkA_Hit.SetActive(false);
    }

    protected override void AttackB() // ���
    {
        // anim.SetTrigger("AttackB_1");

    }

    protected override void AttackC() // ����
    {
        // anim.SetTrigger("AttackC_1");

    }

    protected override void AttackD() // �ָ���
    {
        // anim.SetTrigger("AttackD_1");

    }

    private IEnumerator IESpriteA(SpriteRenderer sprite)
    {
        float a = 0f;

        while (sprite.color.a < 1)
        {
            sprite.color = new Color(1, 0, 0, a);

            a += Time.deltaTime / 1.5f;

            yield return null;
        }
    }

    protected override void Dead()
    {
        
    }
}
