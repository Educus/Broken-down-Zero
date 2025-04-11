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

            if (interaction.transform.Find("Position") == null)
            {
                pushKeyUI.transform.position = interaction.transform.position + new Vector3(1,0,0);
            }
            else
            {
                pushKeyUI.transform.position = interaction.transform.Find("Position").position;
            }
        }

    }
}
