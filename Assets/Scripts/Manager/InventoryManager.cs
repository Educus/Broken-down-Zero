using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Inventory inventory;
    public Inventory inven { get { return inventory; } }

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

    public void GetItem(Item item)
    {
        inventory.GetItem(item);
    }

    private List<DBItem> calculateItem;
    public void InventoryCalculate()
    {
        InventoryClear();
    }

    private void InventoryClear()
    {
        calculateItem = new List<DBItem>();
        calculateItem = inventory.AllClearItemSolt();
    }
}
