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
    [SerializeField] GameObject notBuyButton;

    private ShopListItem buyItem;
    void Start()
    {
        shop.SetActive(false);
        buyButton.SetActive(false);
        notBuyButton.SetActive(false);
    }

    private void Update()
    {
        if (shop.activeSelf == true)
            if (InventoryManager.Instance.inven.gameObject.activeSelf == false)
                shop.SetActive(false);

    }

    public void ActiveShop()
    {
        if (shop.activeSelf == InventoryManager.Instance.inven.gameObject.activeSelf)
        {
            InventoryManager.Instance.OpenInven();
        }

        buyButton.SetActive(false);
        notBuyButton.SetActive(false);
        itemInformation.SetActive(false);
        shop.SetActive(!shop.activeSelf);
    }

    public void CloseShop()
    {
        if (shop.activeSelf == false) return;

        buyButton.SetActive(false);
        notBuyButton.SetActive(false);
        itemInformation.SetActive(false);
        shop.SetActive(false);
        InventoryManager.Instance.inven.gameObject.SetActive(false);
    }

    public void BuyItem(ShopListItem item)
    {
        buyItem = item;
        buyButton.SetActive(true);
        buyItemText.text = item.dbItem.ItemName + "을(를) " + "<sprite=1>" + item.dbItem.ItemPrice + "에" + "\n구매하시겠습니까?";
    }

    public void BuyButton(bool value)
    {
        if (value)
        {
            if (GamePlayerDataManager.Instance.SetManaStone(buyItem.dbItem.ItemPrice))
            {
                Item item = new Item();
                item.SetItem(buyItem.dbItem, true);

                InventoryManager.Instance.GetItem(item);
                Destroy(buyItem.gameObject);
            }
            else
            {
                notBuyButton?.SetActive(true);
                buyItem = null;
            }
        }
        else
        {
            buyItem = null;
        }

        buyButton.SetActive(false);
    }

    public void NotBuy()
    {
        notBuyButton.SetActive(false);
    }
}
