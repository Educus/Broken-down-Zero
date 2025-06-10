using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private bool conditions = true;            // 조건여부(이전의 조건이 만족하였는가?)
    [SerializeField] private UpgradeSlot[] slot = null;         // 조건 대상

    [SerializeField] private int upgradeMax = 5;                // 업그레이드 총 가능 횟수
    public int upgradeCount { get; private set; } = 0;          // 업그레이드 횟수
    [SerializeField] TMP_Text upgradeText;                      // 업그레이드 텍스트
    [HideInInspector] public bool upgrade { get; private set; } // 업그레이드 여부

    [SerializeField] private int needGear = 5;                  // 업그레이드 할 때 필요한 기어 수
    [SerializeField] public float progress = 0.30f;             // 업그레이드 당 상승하는 진행률

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
        if (conditions) // 조건 여부
        {
            // 앞의 업그레이드가 이루어지지 않았다면 붉은색
            if (slot[0]?.upgrade == false)
            {
                image.color = Color.red;
                return;
            }

            // 앞의 업그레이드가 이루어졌고, 다른 루트의 업그레이드가 이루어졌다면 붉은색
            if (slot.Length > 1 && slot[1]?.upgradeCount > 0)
            {
                image.color = Color.black;
                return;
            }
        }

        // 업그레이드가 되었을 경우 초록색, 아닐경우 흰색
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
        // 업그레이드가 최고치일때, 진행률을 초과할때 업그레이드 불가
        if (upgrade || !setUpgrade)
            return;

        if (conditions) // 조건 여부
        {
            // 앞의 업그레이드가 이루어지지 않았다면 업그레이드 불가
            if (slot[0]?.upgrade == false)
                return;

            // 앞의 업그레이드가 이루어졌고, 다른 루트의 업그레이드가 이루어졌다면 업그레이드 불가
            if (slot.Length > 1 && slot[1]?.upgradeCount > 0)
                return;
        }

        if(GamePlayerDataManager.Instance.SetGear(needGear)) // 보유기어 >= 필요기어인 경우 강화
        {
            upgradeCount++;
        }
    }
}
