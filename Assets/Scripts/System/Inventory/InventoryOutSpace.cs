using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryOutSpace : MonoBehaviour, IDropHandler
{
    // ���� ����
    [SerializeField] private GameObject itemObjectPrefab;
    [SerializeField] private WaitForSeconds inactiveTime = new WaitForSeconds(2.5f);

    public void OnDrop(PointerEventData eventData)
    {
        if (ChangeSlot.Instance.currentSlot == null) return;
        if (ChangeSlot.Instance.currentSlot.dbItem == null) return;

        // �� ������ ������ ������ ������Ʈ �����ϱ�
        // ChangeSlot.Instance.currentSlot.dbItem;
        GameObject item = Instantiate(itemObjectPrefab);
        item.transform.position = GameManager.Instance.player.transform.position + new Vector3(0,item.GetComponent<CircleCollider2D>().radius + 0.1f,0);
        item.GetComponent<Item>().indbItem = ChangeSlot.Instance.currentSlot.dbItem;
        item.GetComponent<Item>().OnSimulated();

        ChangeSlot.Instance.currentSlot.ClearSolt();
        ChangeSlot.Instance.SetColor(0);
        ChangeSlot.Instance.currentSlot = null;
    }

    private IEnumerator IESimulated(GameObject item)
    {
        yield return inactiveTime;

        item.GetComponent<Rigidbody2D>().simulated = true;
    }
}
