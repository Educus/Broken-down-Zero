using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerDataManager : Singleton<GamePlayerDataManager>
{
    // 무기
    [HideInInspector] public DBItem playerWeapon { get; private set; }

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


    // 플레이어의 스탯(기본스탯+아이템스탯), 보유재화(마석, 기어), 장착중인 무기 같은 모든 정보 다루기
    // 이후 플레이어의 공격데미지, 인벤토리 스탯표시 수정하기 <- 이곳에서 작성된 기록 가져가기
    // 그리고 무기에 따른 플레이어 idle 변경 <- 이건 인벤토리에서

    private void Update()
    {
        Weapons();
        Stats();
    }

    private void Weapons()
    {
        playerWeapon = InventoryManager.Instance.inven.getWeapon?.dbItem ?? null;
    }
    private void Stats()
    {
        playerHp = (int)DBPlayer.Instance.maxHp + (playerWeapon?.ItemHp ?? 0);
        playerPower = (int)DBPlayer.Instance.power + (playerWeapon?.ItemPower ?? 0);
        playerDefence = (int)DBPlayer.Instance.defence + (playerWeapon?.ItemDefence ?? 0);
        playerSpeed = (int)DBPlayer.Instance.moveSpeed + (playerWeapon?.ItemSpeed ?? 0);
        playerATKSpeed = DBPlayer.Instance.attackSpeed + (playerWeapon?.ItemATKSpeed ?? 0);
        playerCri = (int)DBPlayer.Instance.critical + (playerWeapon?.ItemCri ?? 0);
        playerCriDamage = (int)DBPlayer.Instance.criticalDamage + (playerWeapon?.ItemCriDamage ?? 0);
        playerAvoid = DBPlayer.Instance.avoidance + (playerWeapon?.ItemAvoid ?? 0);

    }

    // 재화를 외부에서 얻을 때, 쓸 때
    public void GetManaStone()
    {

    }
    public void SetManaStone()
    {

    }
    public void GetGear()
    {

    }
    public void SetGear()
    {

    }

}
