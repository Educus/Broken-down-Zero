using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerNPC : NPC
{
    [SerializeField] PlayerUpgrade upgrade;

    public override void Interact()
    {
        upgrade.ActiveShop();
    }
}
