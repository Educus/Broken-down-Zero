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
    [Header("아이템 ID")]
    [SerializeField] private int mItemID;

    public int ItemID { get { return mItemID; } }

    [Header("아이템 중첩 여부")]
    [SerializeField] private bool mCanOverlap;
    public bool CanOverlap { get { return mCanOverlap; } }

    [Header("아이템 사용 여부")]
    [SerializeField] private bool mIsInteractivity;
    public bool IsInteractivity { get { return mIsInteractivity; } }

    [Header("아이템 사용시 소멸 여부")]
    [SerializeField] private bool mIsConsumable;
    public bool IsConsumable { get { return mIsConsumable; } }

    [Header("아이템 사용시 쿨타임")]
    [SerializeField] private float mItemCooltime = -1;
    public float ItemCooltime { get { return mItemCooltime; } }

    [Header("아이템 타입")]
    [SerializeField] private ItemType mItemType;
    public ItemType ItemType { get { return mItemType; } }

    [Header("아이템 이미지")]
    [SerializeField] private Sprite mItemImage;
    public Sprite ItemImage { get { return mItemImage; } }

    [Header("아이템 프리팹")]
    [SerializeField] private GameObject mItemPrefab;
    public GameObject ItemPrefab { get { return mItemPrefab; } }
}
