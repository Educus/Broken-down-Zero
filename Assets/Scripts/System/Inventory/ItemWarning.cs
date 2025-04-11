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

        // 아이템을 버렸을 때 경고창이 뜨고
        // 예를 누르면 아이템 삭제
        // 아니오를 누르면 원상복귀

    }

}
