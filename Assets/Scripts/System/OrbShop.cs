using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrbShop : MonoBehaviour
{
    [SerializeField] GameObject shop;
    [SerializeField] GameObject itemInformation;
    [SerializeField] GameObject buyButton;
    [SerializeField] TMP_Text buyItemText;

    private ShopListItem buyItem;
    void Start()
    {
        shop.SetActive(false);
        buyButton.SetActive(false);
    }

    public void ActiveShop()
    {
        if (shop.activeSelf == InventoryManager.Instance.inven.gameObject.activeSelf)
        {
            InventoryManager.Instance.OpenInven();
        }

        buyButton.SetActive(false);
        itemInformation.SetActive(false);
        shop.SetActive(!shop.activeSelf);
    }

    public void CloseShop()
    {
        buyButton.SetActive(false);
        itemInformation.SetActive(false);
        shop.SetActive(false);
    }

    public void BuyItem(ShopListItem item)
    {
        buyItem = item;
        buyButton.SetActive(true);
        buyItemText.text = item.dbItem.ItemName + "을(를) " + item.dbItem.ItemPrice + "에" + "\n구매하시겠습니까?";
    }

    public void BuyButton(bool value)
    {
        if (value)
        {
            Item item = new Item();
            item.SetItem(buyItem.dbItem, true);

            InventoryManager.Instance.GetItem(item);
            Debug.Log("구매");
            Destroy(buyItem.gameObject);
        }
        else
        {
            buyItem = null;
        }

        buyButton.SetActive(false);
    }
}
