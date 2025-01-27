using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Add DB/Enemy")]
public class DBEnemy : ScriptableObject
{
    [Header("���� ID")]
    [SerializeField] private int mEnemyID;
    public int EnemyID { get { return mEnemyID; } }

    [Header("���� ���ݷ�")]
    [SerializeField] private float mEnemyATK;
    public float EnemyATK { get { return mEnemyATK; } }

    [Header("���� ����")]
    [SerializeField] private float mEnemyDEF;
    public float EnemyDEF { get { return mEnemyDEF; } }

    [Header("���� �̵� �ӵ�")]
    [SerializeField] private float mEnemySPEED;
    public float EnemySPEED { get { return mEnemySPEED; } }

    [Header("���� ���� �ӵ�")]
    [SerializeField] private float mEnemyATKSPEED;
    public float EnemyATKSPEED { get { return mEnemyATKSPEED; } }

    [Header("���� �̹���")]
    [SerializeField] private Sprite mEnemyImage;
    public Sprite EnemyImage { get { return mEnemyImage; } }

    [Header("���� ������")]
    [SerializeField] private GameObject mEnemyPrefab;
    public GameObject EnemyPrefab { get { return mEnemyPrefab; } }
}
