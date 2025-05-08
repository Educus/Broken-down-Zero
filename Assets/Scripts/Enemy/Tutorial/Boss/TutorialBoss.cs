using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Boss
{
    // 스킬A 지면폭발
    [SerializeField] GameObject atkA_Hit;
    private SpriteRenderer atkA_sprite;
    // 스킬B 사격
    [SerializeField] GameObject atkB_Shot;
    [SerializeField] GameObject atkB_Hit;
    [SerializeField] EnemyBullet bullet;
    private SpriteRenderer atkB_sprite;
    // 스킬C 돌진
    [SerializeField] GameObject atkC_Hit;
    [SerializeField] GameObject atkC_Damage;
    private float rushSpeed = 20f;  // 돌진 속도
    private SpriteRenderer atkC_sprite;
    // 스킬D 내려치기
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
    // 플레이어가 보스존에 들어왔을 때 isPlaying, isMove값 true로 바꾸기
    public void Playing()
    {
        isPlaying = true;
        isMove = true;
        anim.SetBool("Move", true);
    }
    protected override void Update()
    {
        anim.SetInteger("VelocityX", (int)rigid.velocity.x);

        if(isPlaying) // 살아있을 때
        {
            if (!isAttacking) // 공격중이 아닐 때
            {
                Move(speed);
                Attack();

                // 공격 타이머
                attackTimer -= Time.deltaTime;

                if (attackTimer < 0)
                {
                    attackTimer = atkSpeed;
                    isAttack = true;
                }

                // 공격이 끝난 후 플레이어가 죽었을 때
                if (!GameManager.Instance.tutorial && player.playerHp <= 0)
                {
                    GetComponent<TutorialEndEvent>().enabled = true;
                    GetComponent<TutorialBoss>().enabled = false;
                }
            }
        }
        else // 살아있지 않을 때
        {
            Move(0f);
        }


    }
    protected override void Move(float value)
    {
        if (isMove) // 움직이는가?
        {
            if (rush)
            {
                rigid.velocity = new Vector2(sprite.flipX ? rushSpeed : -rushSpeed, 0f);
            }
            else
            {
                LookPlayer();

                if (Mathf.Abs(transform.position.x - player.transform.position.x) > 1.5f) // 플레이어와의 거리가 *이상일 때 움직임
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

    // 보스 패턴
    // 
    // 움직인다고 가정
    // 
    // 플레이어와 일정거리 이상일 때 플레이어를 향해 움직인다 <- 만들예정
    // 일정시간마다 기술을 사용한다 <- 0
    // 기술을 움직이지 않고 제자리에서 사용한다 <- 0
    // 단 기술 C 돌진과 주먹질D의 경우가...
    protected override void Attack()
    {
        if (!isAttack) return;  // 공격명령이 안들어왔다면 return;
        if (isAttacking) return; // 공격중이라면 return;

        isAttack = false;
        isAttacking = true;
        Move(0f); // 공격 시작 전 멈추기

        int value = Random.Range(0, 4); // 무작위 패턴 공격 <- 지금은 동일 확률

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
    protected override void AttackA() // 지면 폭발
    {
        LookPlayer();
        anim.SetTrigger("AttackA_1");
        attackCoroutine = StartCoroutine(IESpriteA(atkA_sprite));
    }
    public void AttackA_2()
    {
        // 공격2 실행, 즉시 피해, 색 초기화 및 비활성화
        atkA_Hit.GetComponent<Boss_SingleDamage?>().HitDamage(atk);
        anim.SetTrigger("AttackA_2");
        StopCoroutine(attackCoroutine);
        atkA_sprite.color = new Color(1, 0, 0, 0f);
    }

    protected override void AttackB() // 사격
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

    protected override void AttackC() // 돌진
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
    protected override void AttackD() // 주먹질
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
        // 불사
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
