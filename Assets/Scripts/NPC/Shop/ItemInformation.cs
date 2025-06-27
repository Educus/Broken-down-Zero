using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformation : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [Header("아이템 정보")]
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text item_effect;
    [SerializeField] private TMP_Text item_value;

    private void Awake()
    {
        background.SetActive(false);
    }
    public void OnItemInformation(ShopListItem item)
    {
        Information(item.dbItem);
        background.SetActive(true);
    }
    public void OffItemInformation()
    {
        background.SetActive(false);
    }

    private void Information(DBItem item)
    {
        itemName.text = item.ItemName;

        item_effect.text = "";
        item_value.text = "";

        if (item.ItemHp != 0)
        {
            item_effect.text += "최대체력\n";
            item_value.text += "+" + item.ItemHp.ToString() + "\n";
        }
        if (item.ItemPower != 0)
        {
            item_effect.text += "공격력\n";
            item_value.text += "+" + item.ItemPower.ToString() + "\n";
        }
        if (item.ItemDefence != 0)
        {
            item_effect.text += "방어력\n";
            item_value.text += "+" + item.ItemDefence.ToString() + "\n";
        }
        // 속도 제외
        if (item.ItemATKSpeed != 0)
        {
            item_effect.text += "공격속도\n";
            item_value.text += "+" + item.ItemATKSpeed.ToString() + "\n";
        }
        if (item.ItemCri != 0)
        {
            item_effect.text += "치명타확룰\n";
            item_value.text += "+" + item.ItemCri.ToString() + "\n";
        }
        if (item.ItemCriDamage != 0)
        {
            item_effect.text += "치명타피해\n";
            item_value.text += "+" + item.ItemCriDamage.ToString() + "\n";
        }
        if (item.ItemAvoid != 0)
        {
            item_effect.text += "회피율";
            item_value.text += "+" + item.ItemAvoid.ToString();
        }
    }
}
