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
    public readonly float maxHp           = 100; // 최대체력
    public readonly float power           = 35; // 공격력
    public readonly float defence         = 0; // 방어력
    public readonly float moveSpeed       = 5f; // 이동속도
    public readonly float attackSpeed     = 0; // 공격속도
    public readonly float jumpPower       = 10f; // 점프력
    public readonly float dashRange       = 20f; // 대쉬거리
    public readonly float critical        = 0; // 크리티컬 확률
    public readonly float criticalDamage  = 0; // 크리티컬 피해량
    public readonly float avoidance       = 0; // 회피율

    [HideInInspector] public string[] playerStatKorean = {"체력", "공격력", "방어력", "이동속도", "공격속도", "치명타확률", "치명타피해량", "회피율"};
}
