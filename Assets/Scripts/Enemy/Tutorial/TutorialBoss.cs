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
        atkA_Hit.SetActive(true);
        attackCoroutine = StartCoroutine(IESpriteA(atkA_sprite));
    }
    public void AttackA_2()
    {
        // 공격2 실행, 즉시 피해, 색 초기화 및 비활성화
        atkA_Hit.GetComponent<BossAttackZone>().HitDamage(atk);
        anim.SetTrigger("AttackA_2");
        StopCoroutine(attackCoroutine);
        atkA_sprite.color = new Color(1, 0, 0, 0f);
        atkA_Hit.SetActive(false);
    }

    protected override void AttackB() // 사격
    {
        // anim.SetTrigger("AttackB_1");

    }

    protected override void AttackC() // 돌진
    {
        // anim.SetTrigger("AttackC_1");

    }

    protected override void AttackD() // 주먹질
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
