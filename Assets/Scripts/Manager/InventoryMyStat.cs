using TMPro;
using UnityEngine;

public class InventoryMyStat : MonoBehaviour
{
    [SerializeField] private TMP_Text myHp;
    [SerializeField] private TMP_Text myPower;
    [SerializeField] private TMP_Text myDefence;
    [SerializeField] private TMP_Text myMoveSpeed;
    [SerializeField] private TMP_Text myAttackSpeed;
    [SerializeField] private TMP_Text myCritical;
    [SerializeField] private TMP_Text myCriticalDamage;
    [SerializeField] private TMP_Text myAvoidance;

    void Update()
    {
        MyStats();
    }

    private void MyStats()
    {
        myHp.text = "체력\n" + GamePlayerDataManager.Instance.playerHp;
        myPower.text = "공격력\n" + GamePlayerDataManager.Instance.playerPower;
        myDefence.text = "방어력\n" + GamePlayerDataManager.Instance.playerDefence;
        myMoveSpeed.text = "이동 속도\n" + GamePlayerDataManager.Instance.playerSpeed;
        myAttackSpeed.text = "공격 속도\n" + GamePlayerDataManager.Instance.playerATKSpeed;
        myCritical.text = "치명타 확률\n" + GamePlayerDataManager.Instance.playerCri + "%";
        myCriticalDamage.text = "치명타 피해량\n" + GamePlayerDataManager.Instance.playerCriDamage;
        myAvoidance.text = "회피율\n" + GamePlayerDataManager.Instance.playerAvoid + "%";
    }
}
