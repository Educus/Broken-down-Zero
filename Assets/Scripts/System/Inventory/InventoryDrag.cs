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

    // �巡�װ� ������ OnDrop�Լ��� ���ٸ� OnEndDrag�Լ� �ߵ�, �ִٸ� OnDrop�Լ��� �ߵ�
    // UI�̹����� Raycast_Target�� false��� OnDrop�Լ��� �־ �ߵ���������
    public void OnEndDrag(PointerEventData eventData)
    {
        if (slot.dbItem == null) return;

        ChangeSlot.Instance.SetColor(0);
        ChangeSlot.Instance.currentSlot = null;
    }

    public void OnDrop(PointerEventData eventData)// <- �巡�� �� ��ü
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
