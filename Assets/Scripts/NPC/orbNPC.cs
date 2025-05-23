using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbNPC : NPC
{
    [SerializeField] OrbShop orbShop;
    public override void Interact()
    {
        orbShop.ActiveShop();
    }
}
