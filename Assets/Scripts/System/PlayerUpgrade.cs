using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject shop;

    [SerializeField] private TMP_Text progress;
    [SerializeField] private UpgradeSlot[] slot;

    private void Start()
    {
        shop.SetActive(false);
    }
    private void Update()
    {
        Progress();
    }

    private void Progress()
    {
        // 업그레이드 비율 표시
        int upgradeCount = 0;

        foreach (var upgrade in slot)
        {
            if (upgrade.upgrade)
            {
                upgradeCount++;
            }
        }
        
        // 업그레이드 비율 중 중복된 슬롯 6개 제외
        float rate = (float)upgradeCount / (slot.Length - 6) * 100f;
        progress.text = (int)rate + "%";
    }

    public void ActiveShop()
    {
        shop.SetActive(!shop.activeSelf);
    }

    public void Reset()
    {
        foreach (var upgrade in slot)
        {
            upgrade.upgrade = false;
        }
    }
    public void CloseShop()
    {
        shop.SetActive(false);
    }
}
