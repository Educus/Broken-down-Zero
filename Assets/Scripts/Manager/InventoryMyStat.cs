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
        myHp.text = "ü��\n" + GamePlayerDataManager.Instance.playerHp;
        myPower.text = "���ݷ�\n" + GamePlayerDataManager.Instance.playerPower;
        myDefence.text = "����\n" + GamePlayerDataManager.Instance.playerDefence;
        myMoveSpeed.text = "�̵� �ӵ�\n" + GamePlayerDataManager.Instance.playerSpeed;
        myAttackSpeed.text = "���� �ӵ�\n" + GamePlayerDataManager.Instance.playerATKSpeed;
        myCritical.text = "ġ��Ÿ Ȯ��\n" + GamePlayerDataManager.Instance.playerCri + "%";
        myCriticalDamage.text = "ġ��Ÿ ���ط�\n" + GamePlayerDataManager.Instance.playerCriDamage;
        myAvoidance.text = "ȸ����\n" + GamePlayerDataManager.Instance.playerAvoid + "%";
    }
}
