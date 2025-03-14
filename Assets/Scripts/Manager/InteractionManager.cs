using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    [SerializeField] GameObject pushKeyUI;
    [SerializeField] public Interaction interaction;

    void Update()
    {
        if (interaction == null)
        {
            pushKeyUI.SetActive(false);
        }
        else
        {
            pushKeyUI.SetActive(true);
            pushKeyUI.transform.position = interaction.transform.position + new Vector3(1,0,0);
        }

    }
}
