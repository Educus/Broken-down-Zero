using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Boss : MonoBehaviour, IHitable
{
    [SerializeField] private DBEnemy dbEnemy;   // ���� ����
    [SerializeField] private Slider hpBar;      // ���� ü�¹�

    protected Player player;  // �÷��̾�

    protected float bossID;     // ����ID
    protected float maxHp;      // �ִ� ü��
    protected float nowHp;      // ���� ü��
    protected float atk;        // ���ݷ�
    protected float atkSpeed;   // ���ݼӵ�
    protected float def;        // ����
    protected float speed;      // �̵��ӵ�

    protected Rigidbody2D rigid;
    protected SpriteRenderer sprite;
    protected Animator anim;

    [HideInInspector]
    public bool isPlaying = false; // ��������(�ܺο�)
    [HideInInspector]
    public bool isMove = false; // �̵�����(�ܺο�)
    [HideInInspector]
    public bool isAttack = false; // ��������(�ܺο�)

    protected float attackTimer;
    protected bool isAttacking = false; // ������(���ο�)

    private void Awake()
    {
        player = GameManager.Instance.player?.GetComponent<Player?>();

        bossID = dbEnemy.EnemyID;
        maxHp = dbEnemy.EnemyHp;
        nowHp = maxHp;
        atk = dbEnemy.EnemyATK;
        atkSpeed = dbEnemy.EnemyATKSPEED;
        def = dbEnemy.EnemyDEF;
        speed = dbEnemy.EnemySPEED;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        attackTimer = atkSpeed;
    }

    protected virtual void Update()
    {
        if (isPlaying) // ������� ��
        {
            if (!isAttacking) // �������� �ƴ� ��
            {
                Move(speed);
                Attack();
            }

            // ���� Ÿ�̸� (atkSpeed���� ����)
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                attackTimer = atkSpeed; 
                isAttack = true;
            }
        }
        else // ������� ���� ��
        {
            Move(0f);
        }
    }

    /// <summary>
    /// isAttacking�� ����Ͽ� �������� �ƴ� �� �������� ����� �������϶��� �������� �ʰ� �����
    /// </summary>
    protected abstract void Move(float value);

    protected virtual void Attack()
    {
        if (!isAttack) return;  // ���ݸ���� �ȵ��Դٸ� return;
        if (isAttacking) return; // �������̶�� return;

        isAttack = false;
        isAttacking = true;
        Move(0f); // ���� ���� �� ���߱�

        int value = Random.Range(0, 3); // ������ ���� ���� <- ������ ���� Ȯ��

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
                AttackD();
                break;
            default:
                break;
        }
    }

    protected abstract void AttackA();
    protected abstract void AttackB();
    protected abstract void AttackC();
    protected abstract void AttackD();
    public virtual void EndAttack() // isAttacking ����
    {
        isAttacking = false;
    }
    protected abstract void Dead();

    public void IDamage(float damage)
    {
        if (!isPlaying) return;

        rigid.velocity = new Vector2(0f, 0f);
        nowHp -= damage;

        hpBar.gameObject.SetActive(true);
        hpBar.value = nowHp / maxHp;

        if (nowHp > 0)
            anim.SetTrigger("Hit");
        else
            Dead();

        EndAttack();
    }
}
