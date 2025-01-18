using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBEnemy : Singleton<DBEnemy>
{
    private int hp, power, defence, moveSpeed, attackSpeed;

    public int[] goblin()
    {
        hp          = 0;
        power       = 0;
        defence     = 0;
        moveSpeed   = 0;
        attackSpeed = 0;

        return new int[] { hp, power, defence,moveSpeed, attackSpeed };
    }

}
