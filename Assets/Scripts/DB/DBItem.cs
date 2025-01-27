using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum ItemType
{
    NONE        = 0b0,
    SKILL       = 0b1,

    WEAPON      = 0b10
}

[CreateAssetMenu(fileName = "Item", menuName = "Add DB/Item")]
public class DBItem : ScriptableObject
{
    [Header("������ ID")]
    [SerializeField] private int mItemID;

    public int ItemID { get { return mItemID; } }

    [Header("������ ��ø ����")]
    [SerializeField] private bool mCanOverlap;
    public bool CanOverlap { get { return mCanOverlap; } }

    [Header("������ ��� ����")]
    [SerializeField] private bool mIsInteractivity;
    public bool IsInteractivity { get { return mIsInteractivity; } }

    [Header("������ ���� �Ҹ� ����")]
    [SerializeField] private bool mIsConsumable;
    public bool IsConsumable { get { return mIsConsumable; } }

    [Header("������ ���� ��Ÿ��")]
    [SerializeField] private float mItemCooltime = -1;
    public float ItemCooltime { get { return mItemCooltime; } }

    [Header("������ Ÿ��")]
    [SerializeField] private ItemType mItemType;
    public ItemType ItemType { get { return mItemType; } }

    [Header("������ �̹���")]
    [SerializeField] private Sprite mItemImage;
    public Sprite ItemImage { get { return mItemImage; } }

    [Header("������ ������")]
    [SerializeField] private GameObject mItemPrefab;
    public GameObject ItemPrefab { get { return mItemPrefab; } }
}
