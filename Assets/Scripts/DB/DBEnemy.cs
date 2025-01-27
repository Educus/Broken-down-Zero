using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Add DB/Enemy")]
public class DBEnemy : ScriptableObject
{
    [Header("몬스터 ID")]
    [SerializeField] private int mEnemyID;
    public int EnemyID { get { return mEnemyID; } }

    [Header("몬스터 공격력")]
    [SerializeField] private float mEnemyATK;
    public float EnemyATK { get { return mEnemyATK; } }

    [Header("몬스터 방어력")]
    [SerializeField] private float mEnemyDEF;
    public float EnemyDEF { get { return mEnemyDEF; } }

    [Header("몬스터 이동 속도")]
    [SerializeField] private float mEnemySPEED;
    public float EnemySPEED { get { return mEnemySPEED; } }

    [Header("몬스터 공격 속도")]
    [SerializeField] private float mEnemyATKSPEED;
    public float EnemyATKSPEED { get { return mEnemyATKSPEED; } }

    [Header("몬스터 이미지")]
    [SerializeField] private Sprite mEnemyImage;
    public Sprite EnemyImage { get { return mEnemyImage; } }

    [Header("몬스터 프리팹")]
    [SerializeField] private GameObject mEnemyPrefab;
    public GameObject EnemyPrefab { get { return mEnemyPrefab; } }
}
