using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayerDataManager : Singleton<GamePlayerDataManager>
{
    [SerializeField] public RuntimeAnimatorController[] playerAnim;
    // 무기
    [HideInInspector] public DBItem playerWeapon { get; private set; }
    // 장신구
    [HideInInspector] public DBItem[] playerAccessories { get; private set; }

    // 스탯
    [HideInInspector] public int playerHp { get; private set;}
    [HideInInspector] public int playerPower { get; private set;}
    [HideInInspector] public int playerDefence { get; private set;}
    [HideInInspector] public int playerSpeed { get; private set;}
    [HideInInspector] public float playerATKSpeed { get; private set;}
    [HideInInspector] public int playerCri { get; private set;}
    [HideInInspector] public int playerCriDamage { get; private set;}
    [HideInInspector] public float playerAvoid { get; private set; }

    // 재화
    [HideInInspector] public int manaStone { get; private set;}
    [HideInInspector] public int gear { get; private set; }

    private void Start()
    {
        manaStone = 5000;
        gear = 500;
    }
    private void Update()
    {
        Weapons();
        Accessories();
        Stats();
    }

    private void Weapons()
    {
        playerWeapon = InventoryManager.Instance.inven.getWeapon?.dbItem ?? null;
    }
    private void Accessories()
    {
        playerAccessories = InventoryManager.Instance.inven.getAccessories
                            .Where(slot => slot != null && slot.dbItem != null).Select(slot => slot.dbItem).ToArray();
    }
    private void Stats()
    {
        playerHp = (int)DBPlayer.Instance.maxHp + (playerWeapon?.ItemHp ?? 0) + playerAccessories.Sum(item => item.ItemHp);
        playerPower = (int)DBPlayer.Instance.power + (playerWeapon?.ItemPower ?? 0) + playerAccessories.Sum(item => item.ItemPower);
        playerDefence = (int)DBPlayer.Instance.defence + (playerWeapon?.ItemDefence ?? 0) + playerAccessories.Sum(item => item.ItemDefence);
        playerSpeed = (int)DBPlayer.Instance.moveSpeed + (playerWeapon?.ItemSpeed ?? 0) + playerAccessories.Sum(item => item.ItemSpeed);
        playerATKSpeed = DBPlayer.Instance.attackSpeed + (playerWeapon?.ItemATKSpeed ?? 0) + playerAccessories.Sum(item => item.ItemATKSpeed);
        playerCri = (int)DBPlayer.Instance.critical + (playerWeapon?.ItemCri ?? 0) + playerAccessories.Sum(item => item.ItemCri);
        playerCriDamage = (int)DBPlayer.Instance.criticalDamage + (playerWeapon?.ItemCriDamage ?? 0) + playerAccessories.Sum(item => item.ItemCriDamage);
        playerAvoid = DBPlayer.Instance.avoidance + (playerWeapon?.ItemAvoid ?? 0) + playerAccessories.Sum(item => item.ItemAvoid);

    }

    // 재화를 외부에서 얻을 때, 쓸 때
    public void GetManaStone(int value)
    {
        manaStone += value;
    }
    public bool SetManaStone(int value)
    {
        if (manaStone >= value)
        {
            manaStone -= value;
            return true;
        }

        return false;
    }
    public void GetGear(int value)
    {
        gear += value;
    }
    public bool SetGear(int value)
    {
        if(gear >= value)
        {
            gear -= value;
            return true;
        }

        return false;
    }

}
