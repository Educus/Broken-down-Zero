using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interaction
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void Interact()
    {
        anim.SetTrigger("Open");
    }
}
