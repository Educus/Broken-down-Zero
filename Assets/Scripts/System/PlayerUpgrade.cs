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
        // ���׷��̵� ���� ǥ��
        int upgradeCount = 0;

        foreach (var upgrade in slot)
        {
            if (upgrade.upgrade)
            {
                upgradeCount++;
            }
        }
        
        // ���׷��̵� ���� �� �ߺ��� ���� 6�� ����
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
