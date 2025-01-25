using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : Interaction
{
    [SerializeField] public int nextStage;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }
    public override void Interact()
    {
        // 이후 수정
        Debug.Log("Potal 상호작용 완료");
        StartCoroutine(SceneController.Instance.AsyncLoad(nextStage));
    }
}
