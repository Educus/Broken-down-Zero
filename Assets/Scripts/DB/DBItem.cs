using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum ItemType
{
    NONE        = 0,
    SKILL       = 1 << 0,

    WEAPON      = 1 << 10,
    ACCESSORY   = 1 << 11,
}

[CreateAssetMenu(fileName = "Item", menuName = "Add DB/Item")]
public class DBItem : ScriptableObject
{
    [System.Serializable]
    public class HpPack {
        public int max_Hp;
        public string max_Mp;
    }
    [Header("아이템 ID")]
    [SerializeField] private int mItemID;
    public int ItemID { get { return mItemID; } }

    [Header("아이템 이름")]
    [SerializeField] private string mItemName;
    public string ItemName { get {  return mItemName; } }

    [Header("아이템 가격")]
    [SerializeField] private int mItemPrice;
    public int ItemPrice { get {  return mItemPrice; } }

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

    [Space(10f)]

    [Header("무기")]
    [SerializeField] private int mItemHp;
    public int ItemHp { get { return mItemHp; } }

    [SerializeField] private int mItemPower;
    public int ItemPower { get { return mItemPower; } }

    [SerializeField] private int mItemDefence;
    public int ItemDefence { get { return mItemDefence; } }

    [SerializeField] private int mItemSpeed;
    public int ItemSpeed { get { return mItemSpeed; } }

    [SerializeField] private float mItemATKSpeed;
    public float ItemATKSpeed { get { return mItemATKSpeed; } }

    [SerializeField] private int mItemCri;
    public int ItemCri { get { return mItemCri; } }

    [SerializeField] private int mItemCriDamage;
    public int ItemCriDamage { get { return mItemCriDamage; } }

    [SerializeField] private int mItemAvoid;
    public int ItemAvoid { get { return mItemAvoid; } }

    [Space(10f)]

    [Header("포션")]
    [SerializeField] private int mHpZen;



}
