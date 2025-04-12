using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private ItemSlot slot;

    private void Awake()
    {
        slot = GetComponent<ItemSlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.dbItem == null) return;

        ChangeSlot.Instance.currentSlot = slot;
        ChangeSlot.Instance.DragSetImage(slot.itemImage);
        ChangeSlot.Instance.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slot.dbItem == null) return;

        ChangeSlot.Instance.transform.position = eventData.position;
    }

    // 드래그가 끝날때 OnDrop함수가 없다면 OnEndDrag함수 발동, 있다면 OnDrop함수만 발동
    // UI이미지에 Raycast_Target이 false라면 OnDrop함수가 있어도 발동되지않음
    public void OnEndDrag(PointerEventData eventData)
    {
        if (slot.dbItem == null) return;

        ChangeSlot.Instance.SetColor(0);
        ChangeSlot.Instance.currentSlot = null;
    }

    public void OnDrop(PointerEventData eventData)// <- 드래그 된 객체
    {
        if (ChangeSlot.Instance.currentSlot == null) return;
        if (ChangeSlot.Instance.currentSlot.dbItem == null) return;
        if (ChangeSlot.Instance.currentSlot == slot) return;

        DBItem item = slot.dbItem;
        slot.ClearSolt();
        slot.AddItem(ChangeSlot.Instance.currentSlot.dbItem);
        ChangeSlot.Instance.currentSlot.ClearSolt();
        
        if(item != null) ChangeSlot.Instance.currentSlot.AddItem(item);

        ChangeSlot.Instance.SetColor(0);
        ChangeSlot.Instance.currentSlot = null;
    }
}
