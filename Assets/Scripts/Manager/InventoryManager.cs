using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private RectTransform inventoryBackGround;
    public Inventory inven { get { return inventory; } }
    [HideInInspector] public bool onGui = false;

    private void Start()
    {
        inventory.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OpenInven()
    {
        // if (onGui) { inventoryBackGround.position = new Vector2(-100, 0); }

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
