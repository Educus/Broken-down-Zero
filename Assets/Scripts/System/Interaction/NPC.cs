using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : Interaction
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
    public override void Interact()
    {
        // 이후 수정
        Debug.Log("NPC 상호작용 완료");
    }
}
