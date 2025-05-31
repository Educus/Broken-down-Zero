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
            myHp.text = "체력\n" + (DBPlayer.Instance.maxHp + InventoryManager.Instance.inven.getWeapon.dbItem.ItemHp);
            myPower.text = "공격력\n" + (DBPlayer.Instance.power + InventoryManager.Instance.inven.getWeapon.dbItem.ItemPower);
            myDefence.text = "방어력\n" + (DBPlayer.Instance.defence + InventoryManager.Instance.inven.getWeapon.dbItem.ItemDefence);
            myMoveSpeed.text = "이동 속도\n" + (DBPlayer.Instance.moveSpeed + InventoryManager.Instance.inven.getWeapon.dbItem.ItemSpeed);
            myAttackSpeed.text = "공격 속도\n" + (DBPlayer.Instance.attackSpeed + InventoryManager.Instance.inven.getWeapon.dbItem.ItemATKSpeed);
            myCritical.text = "치명타 확률\n" + (DBPlayer.Instance.critical + InventoryManager.Instance.inven.getWeapon.dbItem.ItemCri) + "%";
            myCriticalDamage.text = "치명타 피해량\n" + (DBPlayer.Instance.criticalDamage + InventoryManager.Instance.inven.getWeapon.dbItem.ItemCriDamage);
            myAvoidance.text = "회피율\n" + (DBPlayer.Instance.avoidance + InventoryManager.Instance.inven.getWeapon.dbItem.ItemAvoid) + "%";
        }
        else {
            myHp.text = "체력\n" + (DBPlayer.Instance.maxHp);
            myPower.text = "공격력\n" + (DBPlayer.Instance.power);
            myDefence.text = "방어력\n" + (DBPlayer.Instance.defence);
            myMoveSpeed.text = "이동 속도\n" + (DBPlayer.Instance.moveSpeed);
            myAttackSpeed.text = "공격 속도\n" + DBPlayer.Instance.attackSpeed;
            myCritical.text = "치명타 확률\n" + (DBPlayer.Instance.critical) + "%";
            myCriticalDamage.text = "치명타 피해량\n" + (DBPlayer.Instance.criticalDamage);
            myAvoidance.text = "회피율\n" + (DBPlayer.Instance.avoidance) + "%";
        }
    }
}
