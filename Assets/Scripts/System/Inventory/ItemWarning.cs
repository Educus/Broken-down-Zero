using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWarning : MonoBehaviour
{
    [SerializeField] GameObject warningObject;
    [SerializeField] TMP_Text warningText;

    private void Start()
    {
        warningObject.SetActive(false);

        // �������� ������ �� ���â�� �߰�
        // ���� ������ ������ ����
        // �ƴϿ��� ������ ���󺹱�

    }

}
