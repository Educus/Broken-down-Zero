using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private DBItem mdbItem;
    public DBItem dbItem { get { return mdbItem; } }

    private int mItemCount;
    public int dbItemCount { get { return mItemCount; } }

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private Image mItemImage;
    public Image itemImage { get {  return mItemImage; } }
    [SerializeField] private Image mCooltimeImage;
    [SerializeField] private TMP_Text mTextCount;

    [Header("아이템 유무에 따른 이미지")]
    [SerializeField] private Image slotImage;
    [SerializeField] private Sprite OnItem;
    [SerializeField] private Sprite OffItem;

    private void Awake()
    {
        SetColor(0);
    }

    private void SetColor(float alpha)
    {
        Color color = mItemImage.color;
        color.a = alpha;
        mItemImage.color = color;

        if (alpha == 1)
            slotImage.sprite = OnItem;
        else if (alpha == 0)
            slotImage.sprite = OffItem;
    }

    public void AddItem(DBItem item, int count = 1)
    {
        mdbItem = item;
        mItemCount = count;
        mItemImage.sprite = mdbItem.ItemImage;

        if(mdbItem.ItemType <= ItemType.WEAPON)
        {
            mTextCount.text = "";
        }
        else
        {
            mTextCount.text = mItemCount.ToString();
        }

        SetColor(1);
    }

    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        mTextCount.text = mTextCount.ToString();

        if (mItemCount <= 0)
            ClearSolt();
    }

    public void ClearSolt()
    {
        mdbItem = null;
        mItemCount = 0;
        mItemImage.sprite = null;
        SetColor(0);

        mTextCount.text = "";
    }
}
