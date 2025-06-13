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
    [Header("������ ID")]
    [SerializeField] private int mItemID;
    public int ItemID { get { return mItemID; } }

    [Header("������ �̸�")]
    [SerializeField] private string mItemName;
    public string ItemName { get {  return mItemName; } }

    [Header("������ ����")]
    [SerializeField] private int mItemPrice;
    public int ItemPrice { get {  return mItemPrice; } }

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

    [Space(10f)]

    [Header("����")]
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

    [Header("����")]
    [SerializeField] private int mHpZen;



}
