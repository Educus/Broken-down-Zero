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

        isAttacking = true;
    }
    protected override void Update()
    {
        anim.SetInteger("VelocityX", (int)rigid.velocity.x);

        if (Input.GetKeyDown(KeyCode.T))
        {
            AttackC();
        }
        if(isPlaying) // 살아있을 때
        {
            if (!isAttacking) // 공격중이 아닐 때
            {
                Move(speed);
                Attack();
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
            LookPlayer();

        }
        else if (rush)
        {
            rigid.velocity = new Vector2(sprite.flipX ? rushSpeed : -rushSpeed, 0f);
        }
        else 
        {
            rigid.velocity = new Vector2 (0f, 0f);
        }
    }

    // 보스 패턴
    // 
    // 움직인다고 가정
    // 
    // 플레이어와 일정거리 이상일 때 플레이어를 향해 움직인다 <- 만들예정
    // 일정시간마다 기술을 사용한다 <- 0
    // 기술을 움직이지 않고 제자리에서 사용한다 <- 0

    private void LookPlayer()
    {
        sprite.flipX = transform.position.x - player.transform.position.x >= 0 ? false : true;
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
    bool rush = false;
    public void AttackC_2()
    {
        StopCoroutine(attackCoroutine);
        atkC_sprite.color = new Color(1, 0, 0, 0f);

        atkC_Damage.GetComponent<Boss_SingleCollisionDamage>().damage = atk;
        rigid.velocity = new Vector2(5f, 0);

        rush = true;
    }
    public void AttackC_End()
    {
        anim.SetBool("Rush", false);
        atkC_Damage.GetComponent<Boss_SingleCollisionDamage>().damage = 0;
        
        rush = false;
    }
    protected override void AttackD() // 주먹질
    {
        LookPlayer();
        anim.SetTrigger("Punch");
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
