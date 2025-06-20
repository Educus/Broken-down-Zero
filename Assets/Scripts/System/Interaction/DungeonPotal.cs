using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPotal : Interaction
{
    [SerializeField] private Transform nextStartPos;
    [SerializeField] private Camera camera;

    public override void Interact()
    {
        GameManager.Instance.player.transform.position = nextStartPos.position;
        camera.transform.position = nextStartPos.position + new Vector3(0,2,0);

    }
}
