using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbNPC : NPC
{
    [SerializeField] OrbShop orbShop;
    public override void Interact()
    {
        orbShop.ActiveShop();
    }
}
