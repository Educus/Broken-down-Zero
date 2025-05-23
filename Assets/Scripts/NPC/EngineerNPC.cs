using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerNPC : NPC
{
    [SerializeField] GameObject playerStat;
    public override void Interact()
    {
        playerStat.SetActive(!playerStat.activeSelf);
    }
}
