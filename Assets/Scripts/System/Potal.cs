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
        // ���� ����
        Debug.Log("Potal ��ȣ�ۿ� �Ϸ�");
        StartCoroutine(SceneController.Instance.AsyncLoad(nextStage));
    }
}
