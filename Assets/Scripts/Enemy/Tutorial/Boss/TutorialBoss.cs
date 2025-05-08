using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Boss
{
    // ��ųA ��������
    [SerializeField] GameObject atkA_Hit;
    private SpriteRenderer atkA_sprite;
    // ��ųB ���
    [SerializeField] GameObject atkB_Shot;
    [SerializeField] GameObject atkB_Hit;
    [SerializeField] EnemyBullet bullet;
    private SpriteRenderer atkB_sprite;
    // ��ųC ����
    [SerializeField] GameObject atkC_Hit;
    [SerializeField] GameObject atkC_Damage;
    private float rushSpeed = 20f;  // ���� �ӵ�
    private SpriteRenderer atkC_sprite;
    // ��ųD ����ġ��
    [SerializeField] GameObject atkD_Hit;
    private SpriteRenderer atkD_sprite;

    private Coroutine attackCoroutine;

    private void Start()
    {
        atkA_sprite = atkA_Hit.GetComponent<SpriteRenderer>();
        atkB_sprite = atkB_Hit.GetComponent<SpriteRenderer>();
        atkC_sprite = atkC_Hit.GetComponent<SpriteRenderer>();
        atkD_sprite = atkD_Hit.GetComponent<SpriteRenderer>();

    }
    // �÷��̾ �������� ������ �� isPlaying, isMove�� true�� �ٲٱ�
    public void Playing()
    {
        isPlaying = true;
        isMove = true;
        anim.SetBool("Move", true);
    }
    protected override void Update()
    {
        anim.SetInteger("VelocityX", (int)rigid.velocity.x);

        if(isPlaying) // ������� ��
        {
            if (!isAttacking) // �������� �ƴ� ��
            {
                Move(speed);
                Attack();

                // ���� Ÿ�̸�
                attackTimer -= Time.deltaTime;

                if (attackTimer < 0)
                {
                    attackTimer = atkSpeed;
                    isAttack = true;
                }

                // ������ ���� �� �÷��̾ �׾��� ��
                if (!GameManager.Instance.tutorial && player.playerHp <= 0)
                {
                    GetComponent<TutorialEndEvent>().enabled = true;
                    GetComponent<TutorialBoss>().enabled = false;
                }
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
            if (rush)
            {
                rigid.velocity = new Vector2(sprite.flipX ? rushSpeed : -rushSpeed, 0f);
            }
            else
            {
                LookPlayer();

                if (Mathf.Abs(transform.position.x - player.transform.position.x) > 1.5f) // �÷��̾���� �Ÿ��� *�̻��� �� ������
                {
                    rigid.velocity = new Vector2(sprite.flipX ? value : -value, 0f);
                }
                else
                {
                    rigid.velocity = new Vector2(0f, 0f);
                }
            }
        }
    }


    private void LookPlayer()
    {
        sprite.flipX = transform.position.x - player.transform.position.x >= 0 ? false : true;
    }

    // ���� ����
    // 
    // �����δٰ� ����
    // 
    // �÷��̾�� �����Ÿ� �̻��� �� �÷��̾ ���� �����δ� <- ���鿹��
    // �����ð����� ����� ����Ѵ� <- 0
    // ����� �������� �ʰ� ���ڸ����� ����Ѵ� <- 0
    // �� ��� C ������ �ָ���D�� ��찡...
    protected override void Attack()
    {
        if (!isAttack) return;  // ���ݸ���� �ȵ��Դٸ� return;
        if (isAttacking) return; // �������̶�� return;

        isAttack = false;
        isAttacking = true;
        Move(0f); // ���� ���� �� ���߱�

        int value = Random.Range(0, 4); // ������ ���� ���� <- ������ ���� Ȯ��

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
                StartCoroutine(IEAttackD());
                break;
            default:
                break;
        }
    }
    protected override void AttackA() // ���� ����
    {
        LookPlayer();
        anim.SetTrigger("AttackA_1");
        attackCoroutine = StartCoroutine(IESpriteA(atkA_sprite));
    }
    public void AttackA_2()
    {
        // ����2 ����, ��� ����, �� �ʱ�ȭ �� ��Ȱ��ȭ
        atkA_Hit.GetComponent<Boss_SingleDamage?>().HitDamage(atk);
        anim.SetTrigger("AttackA_2");
        StopCoroutine(attackCoroutine);
        atkA_sprite.color = new Color(1, 0, 0, 0f);
    }

    protected override void AttackB() // ���
    {
        LookPlayer();
        anim.SetTrigger("AttackB_1");
        attackCoroutine = StartCoroutine(IESpriteA(atkB_sprite));
    }
    public void AttackB_2()
    {
        bullet.transform.position = atkB_Shot.transform.position;
        bullet.gameObject.SetActive(true);
        bullet.speed = sprite.flipX ? 50 : -50;
        bullet.damage = atk;
        StopCoroutine(attackCoroutine);
        atkB_sprite.color = new Color(1, 0, 0, 0f);
        anim.SetTrigger("AttackB_2");
    }
    public void AttackB_End()
    {
        bullet.speed = 0;
        bullet.damage = 0;
        bullet.gameObject.SetActive(false);
    }

    protected override void AttackC() // ����
    {
        LookPlayer();
        anim.SetBool("Rush", true);
        attackCoroutine = StartCoroutine(IESpriteA(atkC_sprite));
    }
    private bool rush = false;
    public void AttackC_2()
    {
        StopCoroutine(attackCoroutine);
        atkC_sprite.color = new Color(1, 0, 0, 0f);

        atkC_Damage.GetComponent<Boss_SingleCollisionDamage>().damage = atk;
        atkC_Damage.gameObject.SetActive(true);
        rush = true;

        Move(20f);
    }
    public void AttackC_End()
    {
        anim.SetBool("Rush", false);
        atkC_Damage.GetComponent<Boss_SingleCollisionDamage>().damage = 0;
        atkC_Damage.gameObject.SetActive(false);
        
        rush = false;
    }
    private IEnumerator IEAttackD()
    {
        while(Mathf.Abs(transform.position.x - player.transform.position.x) > 3)
        {
            Move(speed);
            yield return null;
        }

        Move(0);
        AttackD();

        yield return null;
    }
    protected override void AttackD() // �ָ���
    {
        LookPlayer();

        anim.SetTrigger("AttackD_1");
        attackCoroutine = StartCoroutine(IESpriteA(atkD_sprite));
    }
    public void AttackD_2()
    {
        StopCoroutine(attackCoroutine);
        atkD_sprite.color = new Color(1, 0, 0, 0f);
        atkD_Hit.GetComponent<Boss_SingleDamage?>().HitDamage(atk);
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
        // �һ�
    }


    // test
    public void A()
    {
        AttackA();
    }
    public void B()
    {
        AttackB();
    }
    public void C()
    {
        AttackC();
    }
    public void D()
    {
        AttackD();
    }

}
