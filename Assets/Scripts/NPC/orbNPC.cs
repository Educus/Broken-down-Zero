using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbNPC : NPC
{
    [SerializeField] GameObject orbShop;
    public override void Interact()
    {
        orbShop.SetActive(!orbShop.activeSelf);
    }
}
