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
    public readonly float maxHp           = 0; // 최대체력
    public readonly float power           = 0; // 공격력
    public readonly float defence         = 0; // 방어력
    public readonly float moveSpeed       = 5f; // 이동속도
    public readonly float attackSpeed     = 0; // 공격속도
    public readonly float jumpPower       = 10f; // 점프력
    public readonly float dashRange       = 3f; // 대쉬거리
    public readonly float critical        = 0; // 크리티컬 확률
    public readonly float criticalDamage  = 0; // 크리티컬 피해량
    public readonly float avoidance       = 0; // 회피율

}
