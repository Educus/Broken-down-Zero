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
    [SerializeField] private TMP_Text itemAttack_Text;
    [SerializeField] private TMP_Text itemATKSpeed_Text;
    [SerializeField] private TMP_Text itemCritical_Text;

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
        itemAttack_Text.text = "+" + item.ItemPower.ToString();
        itemATKSpeed_Text.text = item.ItemATKSpeed.ToString();
        itemCritical_Text.text = "+" + item.ItemCri.ToString() + "%";
    }
}
