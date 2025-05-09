using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryOutSpace : MonoBehaviour, IDropHandler
{
    // 이후 수정
    // [SerializeField] private GameObject itemObjectPrefab;
    [SerializeField] private ItemWarning itemWarning;

    public void OnDrop(PointerEventData eventData)
    {
        if (ChangeSlot.Instance.currentSlot == null) return;
        if (ChangeSlot.Instance.currentSlot.dbItem == null) return;

        // 이 정보만 가지고 아이템 오브젝트 생성하기
        // ChangeSlot.Instance.currentSlot.dbItem;
        // GameObject item = Instantiate(itemObjectPrefab);
        // item.transform.position = GameManager.Instance.player.transform.position + new Vector3(0,item.GetComponent<CircleCollider2D>().radius + 0.1f,0);
        // item.GetComponent<Item>().indbItem = ChangeSlot.Instance.currentSlot.dbItem;
        // item.GetComponent<Item>().OnSimulated();

        StartCoroutine(DropItem(eventData));
    }

    private IEnumerator DropItem(PointerEventData eventData)
    {
        int result = 0;
        ItemSlot itemSlot = ChangeSlot.Instance.currentSlot;

        yield return StartCoroutine(itemWarning.DropItem(value => result = value));

        Debug.Log(result);

        if (result == 1)
        {
            ChangeSlot.Instance.currentSlot = itemSlot;
            ChangeSlot.Instance.currentSlot.ClearSolt();
            ChangeSlot.Instance.SetColor(0);
            ChangeSlot.Instance.currentSlot = null;
        }
        else if (result == 2)
        {
            itemSlot = null;
        }
    }
}
