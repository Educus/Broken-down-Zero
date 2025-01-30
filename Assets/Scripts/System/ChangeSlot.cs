using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSlot : Singleton<ChangeSlot>
{
    [HideInInspector] public ItemSlot currentSlot;
    [SerializeField] private Image mItemImage;

    public void DragSetImage(Image itemImage)
    {
        mItemImage.sprite = itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = mItemImage.color;
        color.a = alpha;
        mItemImage.color = color;
    }

}
