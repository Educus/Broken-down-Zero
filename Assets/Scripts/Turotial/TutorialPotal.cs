using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPotal : Interaction
{
    [SerializeField] private GameObject outPotal;
    [SerializeField] private int[] range;
    [SerializeField] private TutorialCamera camera;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    // 튜토리얼 두번째 72,91
    public override void Interact()
    {
        GameManager.Instance.player.transform.position = outPotal.transform.position;
        GameManager.Instance.startingPoint = outPotal;
        camera.minMove = new Vector3(range[0], 0, 0);
        camera.maxMove = new Vector3(range[1], 0, 0);
        camera.transform.position = new Vector2(range[0], camera.transform.position.y);

        GameManager.Instance.tutorial = false;
    }
}
