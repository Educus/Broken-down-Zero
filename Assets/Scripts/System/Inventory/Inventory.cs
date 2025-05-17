using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject myStat;
    private List<TMP_Text> statText = new List<TMP_Text>();

    [SerializeField] private ItemSlot weaponSlots = new ItemSlot();
    public ItemSlot getWeapon { get { return weaponSlots; } }


    [SerializeField] private GameObject bag;
    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Awake()
    {
        foreach (Transform child in myStat.transform)
        {
            statText.Add(child.GetComponent<TMP_Text>());
        }
        foreach (Transform child in bag.transform)
        {
            itemSlots.Add(child.GetComponent<ItemSlot>());
            child.GetComponent<ItemSlot>().ClearSolt();
        }
        weaponSlots.ClearSolt();
    }
    void Update()
    {
        Stat();
        Weapon();
    }

    private void Stat()
    {
        for(int i = 0; i < statText.Count; i++) 
        { 
            // ���� ����
            // statText[i].text = Enum.GetName(typeof(PlayerStat), i) + " : " + "001"; // �����
            statText[i].text = DBPlayer.Instance.playerStatKorean[i] + "\n" + "001";
        }
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
