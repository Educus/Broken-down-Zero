using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWarning : MonoBehaviour
{
    [SerializeField] GameObject warningObject;
    private int warningLevel;
    private bool isButtonClicked = false;

    private void Start()
    {
        warningObject.SetActive(false);

        // �������� ������ �� ���â�� �߰�
        // ���� ������ ������ ����
        // �ƴϿ��� ������ ���󺹱�

    }

    public IEnumerator DropItem(Action<int> callback)
    {
        warningObject.SetActive(true);

        yield return new WaitUntil(() => isButtonClicked);

        callback?.Invoke(warningLevel);

        isButtonClicked = false;
        warningLevel = 0;
    }

    public void DropItemYes()
    {
        warningLevel = 1;
        isButtonClicked = true;
        warningObject.SetActive(false);
    }
    public void DropItemNo()
    {
        warningLevel = 2;
        isButtonClicked = true;
        warningObject.SetActive(false);
    }

}
