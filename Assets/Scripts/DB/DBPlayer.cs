using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStat
{
    HP,
    ATK,
    DEF,
    SPEED,
    ATTACK_SPEED,
    CRITICAL_CHANCE,
    CRITICAL_MULTIPLIER,
    AVOID
}
public class DBPlayer : Singleton<DBPlayer>
{
    public readonly float maxHp           = 0; // �ִ�ü��
    public readonly float power           = 0; // ���ݷ�
    public readonly float defence         = 0; // ����
    public readonly float moveSpeed       = 5f; // �̵��ӵ�
    public readonly float attackSpeed     = 0; // ���ݼӵ�
    public readonly float jumpPower       = 10f; // ������
    public readonly float dashRange       = 3f; // �뽬�Ÿ�
    public readonly float critical        = 0; // ũ��Ƽ�� Ȯ��
    public readonly float criticalDamage  = 0; // ũ��Ƽ�� ���ط�
    public readonly float avoidance       = 0; // ȸ����

}
