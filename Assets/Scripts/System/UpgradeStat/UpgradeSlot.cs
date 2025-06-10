using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private bool conditions = true;            // ���ǿ���(������ ������ �����Ͽ��°�?)
    [SerializeField] private UpgradeSlot[] slot = null;         // ���� ���

    [SerializeField] private int upgradeMax = 5;                // ���׷��̵� �� ���� Ƚ��
    public int upgradeCount { get; private set; } = 0;          // ���׷��̵� Ƚ��
    [SerializeField] TMP_Text upgradeText;                      // ���׷��̵� �ؽ�Ʈ
    [HideInInspector] public bool upgrade { get; private set; } // ���׷��̵� ����

    [SerializeField] private int needGear = 5;                  // ���׷��̵� �� �� �ʿ��� ��� ��
    [SerializeField] public float progress = 0.30f;             // ���׷��̵� �� ����ϴ� �����

    [HideInInspector] public bool setUpgrade = true;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry clickEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        clickEntry.callback.AddListener((enentData) => ClickEvent());
        trigger.triggers.Add(clickEntry);
    }
    private void Update()
    {
        SlotColor();
        UpgradeMax();
    }

    private void SlotColor()
    {
        if (conditions) // ���� ����
        {
            // ���� ���׷��̵尡 �̷������ �ʾҴٸ� ������
            if (slot[0]?.upgrade == false)
            {
                image.color = Color.red;
                return;
            }

            // ���� ���׷��̵尡 �̷������, �ٸ� ��Ʈ�� ���׷��̵尡 �̷�����ٸ� ������
            if (slot.Length > 1 && slot[1]?.upgradeCount > 0)
            {
                image.color = Color.black;
                return;
            }
        }

        // ���׷��̵尡 �Ǿ��� ��� �ʷϻ�, �ƴҰ�� ���
        Color color = upgrade ? Color.green : Color.white;
        image.color = color;
    }
    private void UpgradeMax()
    {
        if (upgradeMax == upgradeCount)
        {
            upgrade = true; 
            upgradeText.text = "Max";
        }
        else 
        {
            upgrade = false;
            if (upgradeCount == 0)
            {
                upgradeText.text = "";
            }
            else
            {
                upgradeText.text = upgradeCount.ToString();
            }
        }
    }

    public void Reset()
    {
        GamePlayerDataManager.Instance.GetGear(needGear * upgradeCount);
        upgradeCount = 0;
    }
    public void ClickEvent()
    {
        // ���׷��̵尡 �ְ�ġ�϶�, ������� �ʰ��Ҷ� ���׷��̵� �Ұ�
        if (upgrade || !setUpgrade)
            return;

        if (conditions) // ���� ����
        {
            // ���� ���׷��̵尡 �̷������ �ʾҴٸ� ���׷��̵� �Ұ�
            if (slot[0]?.upgrade == false)
                return;

            // ���� ���׷��̵尡 �̷������, �ٸ� ��Ʈ�� ���׷��̵尡 �̷�����ٸ� ���׷��̵� �Ұ�
            if (slot.Length > 1 && slot[1]?.upgradeCount > 0)
                return;
        }

        if(GamePlayerDataManager.Instance.SetGear(needGear)) // ������� >= �ʿ����� ��� ��ȭ
        {
            upgradeCount++;
        }
    }
}
