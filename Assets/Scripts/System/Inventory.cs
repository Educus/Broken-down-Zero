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
    private List<Image> itemImage = new List<Image>();
    private List<int> itemsCode = new List<int>();
    private List<int> itemsNum = new List<int>();

    private void Start()
    {
        foreach (Transform child in myStat.transform)
        {
            statText.Add(child.GetComponent<TMP_Text>());
        }
        foreach (Transform child in bag.transform)
        {
            itemImage.Add(child.GetComponent<Image>());
        }
        foreach (var image in itemImage)
        {
            image.color = new Color(1, 1, 1, 0);
        }
    }
    void Update()
    {
        Stat();
        Weapon();
        Item();
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
        int itemcode;
        if (weapon.transform.childCount == 0)
        {
            itemcode = 10000;
        }
        else
        {
            itemcode = weapon.transform.GetChild(0).GetComponent<Item>().itemCode;
        }

        string path = "PlayerImage/PlayerIdle" + itemcode;

        myCharacter.sprite = Resources.Load<Sprite>(path);
    }
    private void Item()
    {
        // 이후 수정
        // string path = "ItemImage/Item";
        string path = "PlayerImage/PlayerIdle" + 10000; // 실험용
        int itemsCount = itemsCode.Count;

        for (int i = 0; i < itemImage.Count; i++)
        {
            if (i >= itemsCount)
            {
                itemImage[i].color = new Color(1, 1, 1, 0);
            }
            else
            {
                itemImage[i].color = new Color(1, 1, 1, 1);
                itemImage[i].sprite = Resources.Load<Sprite>(path);
                // itemImage[i].sprite = Resources.Load<Sprite>(path + itemsCode[i].ToString()); <- 이후 변경
            }
        }
    }

    public void GetItem(int itemCode, int num)
    {
        int value = itemsCode.FindIndex(n => n == itemCode);

        if (value == -1)
        {
            NewItem(itemCode, num);
        }
        else
        {
            AddItem(value, num);
        }
    }

    private void NewItem(int itemCode, int num)
    {
        itemsCode.Add(itemCode);
        itemsNum.Add(num);
    }
    private void AddItem(int value, int num)
    {
        itemsNum[value] += num;
    }
}
