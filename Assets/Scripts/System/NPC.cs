using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interaction
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
    public override void Interact()
    {
        // ���� ����
        Debug.Log("NPC ��ȣ�ۿ� �Ϸ�");
    }
}
