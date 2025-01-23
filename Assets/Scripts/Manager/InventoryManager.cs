using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        inventory.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OpenInven()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
    }

    public void GetItem(int itemCode, int num)
    {
        inventory.GetItem(itemCode, num);
    }
}
