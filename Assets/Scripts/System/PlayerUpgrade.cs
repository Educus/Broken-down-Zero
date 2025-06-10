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

        nowGear.text = "���� ���\n" + GamePlayerDataManager.Instance.gear.ToString();
    }

    private void Progress()
    {
        // ���׷��̵� ���� ǥ��
        float upgradeCount = 0.00f;

        foreach (var upgrade in slot)
        {
            upgradeCount += (upgrade.progress * upgrade.upgradeCount);
        }
        
        // ���׷��̵� ���� �� �ߺ��� ���� 6�� ����
        // float rate = (float)upgradeCount / (slot.Length - 6) * 100f;
        //  progress.text = rate + "%";
        
        // ���׷��̵带 ���� �� ������� �ʰ��ϸ� ���׷��̵� �Ұ�
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
