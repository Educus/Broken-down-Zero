using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot weaponSlots = new ItemSlot();
    public ItemSlot getWeapon { get { return weaponSlots; } }


    [SerializeField] private GameObject bag;
    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField] private TMP_Text nowManaStone;

    private void Awake()
    {
        foreach (Transform child in bag.transform)
        {
            itemSlots.Add(child.GetComponent<ItemSlot>());
            child.GetComponent<ItemSlot>().ClearSolt();
        }
        weaponSlots.ClearSolt();
    }
    void Update()
    {
        Weapon();
        ManaStone();
    }
    private void Weapon()
    {
        int itemID;
        if (weaponSlots.dbItem == null)
        {
            itemID = 10000;
        }
        else
        {
            itemID = weaponSlots.dbItem.ItemID;
        }

        string path = "PlayerImage/PlayerIdle" + itemID.ToString();
    }
    private void ManaStone()
    {
        nowManaStone.text = GamePlayerDataManager.Instance.manaStone.ToString();
    }
    public void GetItem(Item item)
    {
        if (!item.dbItem.CanOverlap)
        {
            NewItem(item);
        }
        else
        {
            AddItem(item);
            if(item != null)
                NewItem(item);
        }

    }

    private void NewItem(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].dbItem == null)
            {
                itemSlots[i].AddItem(item.dbItem);
                item.DestoyItem();
                item = null;
                return;
            }
        }
    }
    private void AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].dbItem != null)
            {
                if (itemSlots[i].dbItem.ItemID == item.dbItem.ItemID)
                {
                    itemSlots[i].UpdateSlotCount(1);
                    item.DestoyItem();
                    item = null;
                    return;
                }
            }
        }
    }

    public List<DBItem> AllClearItemSolt()
    {
        List<DBItem> dbItem = new List<DBItem>();

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.dbItem != null)
            {
                dbItem.Add(slot.dbItem);
                slot.ClearSolt();
            }
        }

        return dbItem;
    }
}
