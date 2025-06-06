using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private bool conditions = true;   // 조건여부(이전의 조건이 만족하였는가?)
    [SerializeField] private UpgradeSlot[] slot = null;  // 조건 대상
    [HideInInspector] public bool upgrade;      // 업그레이드 여부

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
        if (conditions) // 조건 여부
        {
            // 앞의 업그레이드가 이루어지지 않았다면 붉은색
            if (slot[0]?.upgrade == false)
            {
                image.color = Color.red;
                return;
            }

            // 앞의 업그레이드가 이루어졌고, 다른 루트의 업그레이드가 이루어졌다면 붉은색
            if (slot.Length > 1 && slot[1]?.upgrade == true)
            {
                image.color = Color.red;
                return;
            }
        }

        // 업그레이드가 되었을 경우 초록색, 아닐경우 흰색
        Color color = upgrade ? Color.green : Color.white;
        image.color = color;
    }

    public void ClickEvent()
    {
        if (conditions) // 조건 여부
        {
            // 앞의 업그레이드가 이루어지지 않았다면 업그레이드 불가
            if (slot[0]?.upgrade == false)
                return;

            // 앞의 업그레이드가 이루어졌고, 다른 루트의 업그레이드가 이루어졌다면 업그레이드 불가
            if (slot.Length > 1 && slot[1]?.upgrade == true)
                return;
        }

        upgrade = true;
    }
}
