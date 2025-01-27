using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] Image myCharacter;
    [SerializeField] GameObject myStat;
    private List<TMP_Text> statText = new List<TMP_Text>();

    [SerializeField] GameObject weapon;

    [SerializeField] GameObject bag;
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
            // 이후 수정
            statText[i].text = Enum.GetName(typeof(PlayerStat), i) + " : " + "001"; // 실험용
        }
    }
    private void Weapon()
    {
        int itemID;
        if (weapon.transform.childCount == 0)
        {
            itemID = 10000;
        }
        else
        {
            itemID = weapon.transform.GetChild(0).GetComponent<Item>().dbItem.ItemID;
        }

        string path = "PlayerImage/PlayerIdle" + itemID.ToString();

        myCharacter.sprite = Resources.Load<Sprite>(path);
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
}
