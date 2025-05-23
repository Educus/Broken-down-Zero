using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShop : MonoBehaviour
{
    [SerializeField] GameObject shop;
    void Start()
    {
        shop.SetActive(false);
    }

    public void ActiveShop()
    {
        if (shop.activeSelf == InventoryManager.Instance.inven.gameObject.activeSelf)
        {
            InventoryManager.Instance.OpenInven();
        }

        shop.SetActive(!shop.activeSelf);
    }
}
