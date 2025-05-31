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
        if (InventoryManager.Instance.inven.getWeapon.dbItem != null)
        {
            myHp.text = "ü��\n" + (DBPlayer.Instance.maxHp + InventoryManager.Instance.inven.getWeapon.dbItem.ItemHp);
            myPower.text = "���ݷ�\n" + (DBPlayer.Instance.power + InventoryManager.Instance.inven.getWeapon.dbItem.ItemPower);
            myDefence.text = "����\n" + (DBPlayer.Instance.defence + InventoryManager.Instance.inven.getWeapon.dbItem.ItemDefence);
            myMoveSpeed.text = "�̵� �ӵ�\n" + (DBPlayer.Instance.moveSpeed + InventoryManager.Instance.inven.getWeapon.dbItem.ItemSpeed);
            myAttackSpeed.text = "���� �ӵ�\n" + (DBPlayer.Instance.attackSpeed + InventoryManager.Instance.inven.getWeapon.dbItem.ItemATKSpeed);
            myCritical.text = "ġ��Ÿ Ȯ��\n" + (DBPlayer.Instance.critical + InventoryManager.Instance.inven.getWeapon.dbItem.ItemCri) + "%";
            myCriticalDamage.text = "ġ��Ÿ ���ط�\n" + (DBPlayer.Instance.criticalDamage + InventoryManager.Instance.inven.getWeapon.dbItem.ItemCriDamage);
            myAvoidance.text = "ȸ����\n" + (DBPlayer.Instance.avoidance + InventoryManager.Instance.inven.getWeapon.dbItem.ItemAvoid) + "%";
        }
        else {
            myHp.text = "ü��\n" + (DBPlayer.Instance.maxHp);
            myPower.text = "���ݷ�\n" + (DBPlayer.Instance.power);
            myDefence.text = "����\n" + (DBPlayer.Instance.defence);
            myMoveSpeed.text = "�̵� �ӵ�\n" + (DBPlayer.Instance.moveSpeed);
            myAttackSpeed.text = "���� �ӵ�\n" + DBPlayer.Instance.attackSpeed;
            myCritical.text = "ġ��Ÿ Ȯ��\n" + (DBPlayer.Instance.critical) + "%";
            myCriticalDamage.text = "ġ��Ÿ ���ط�\n" + (DBPlayer.Instance.criticalDamage);
            myAvoidance.text = "ȸ����\n" + (DBPlayer.Instance.avoidance) + "%";
        }
    }
}
