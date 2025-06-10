using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject shop;

    [SerializeField] private TMP_Text nowGear;
    [SerializeField] private TMP_Text progress;
    [SerializeField] private UpgradeSlot[] slot;

    private void Start()
    {
        shop.SetActive(false);
    }
    private void Update()
    {
        Progress();

        nowGear.text = "보유 기어\n" + GamePlayerDataManager.Instance.gear.ToString();
    }

    private void Progress()
    {
        // 업그레이드 비율 표시
        float upgradeCount = 0.00f;

        foreach (var upgrade in slot)
        {
            upgradeCount += (upgrade.progress * upgrade.upgradeCount);
        }
        
        // 업그레이드 비율 중 중복된 슬롯 6개 제외
        // float rate = (float)upgradeCount / (slot.Length - 6) * 100f;
        //  progress.text = rate + "%";
        
        // 업그레이드를 했을 때 진행률을 초과하면 업그레이드 불가
        foreach (var upgrade in slot)
        {
            if(upgradeCount + upgrade.progress > 20.00f)
            {
                upgrade.setUpgrade = false;
            }
            else
            {
                upgrade.setUpgrade = true;
            }
        }

        progress.text = upgradeCount.ToString() + "%";
    }
    public void ActiveShop()
    {
        shop.SetActive(!shop.activeSelf);
    }

    public void Reset()
    {
        foreach (var upgrade in slot)
        {
            upgrade.Reset();
        }
    }
    public void CloseShop()
    {
        shop.SetActive(false);
    }
}
