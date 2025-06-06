using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private bool conditions = true;   // ���ǿ���(������ ������ �����Ͽ��°�?)
    [SerializeField] private UpgradeSlot[] slot = null;  // ���� ���
    [HideInInspector] public bool upgrade;      // ���׷��̵� ����

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
            if (slot.Length > 1 && slot[1]?.upgrade == true)
            {
                image.color = Color.red;
                return;
            }
        }

        // ���׷��̵尡 �Ǿ��� ��� �ʷϻ�, �ƴҰ�� ���
        Color color = upgrade ? Color.green : Color.white;
        image.color = color;
    }

    public void ClickEvent()
    {
        if (conditions) // ���� ����
        {
            // ���� ���׷��̵尡 �̷������ �ʾҴٸ� ���׷��̵� �Ұ�
            if (slot[0]?.upgrade == false)
                return;

            // ���� ���׷��̵尡 �̷������, �ٸ� ��Ʈ�� ���׷��̵尡 �̷�����ٸ� ���׷��̵� �Ұ�
            if (slot.Length > 1 && slot[1]?.upgrade == true)
                return;
        }

        upgrade = true;
    }
}
