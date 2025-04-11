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

        // 아이템을 버렸을 때 경고창이 뜨고
        // 예를 누르면 아이템 삭제
        // 아니오를 누르면 원상복귀

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
