using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopListItem : MonoBehaviour
{
    [SerializeField] public DBItem dbItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemPrice;


    void Start()
    {
        SetData();
    }

    public void SetData()
    {
        itemImage.sprite = dbItem.ItemImage;
        itemName.text = dbItem.ItemName;
        itemPrice.text = dbItem.ItemPrice.ToString();
    }

    public void BuyItem()
    {
        Item item = new Item();
        item.SetItem(dbItem, true);

        InventoryManager.Instance.GetItem(item);
        Debug.Log("±¸¸Å");
        Destroy(gameObject);
    }
}
