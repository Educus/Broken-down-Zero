using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class KeySettingText : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> keyText;
    [SerializeField] private List<TMP_Text> buttonText;

    private string[] KeyInputKoreans = {"왼쪽", "오른쪽", "아래", "공격", "점프", "대쉬", "스킬1", "스킬2", "인벤토리", "상호작용"};

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i % 2 == 0)
            {
                keyText.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>());
            }
            else
            {
                buttonText.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>());
            }
        }
    }
    void Start()
    {
        TextChange();
    }

    void Update()
    {
        TextChange();
    }

    private void TextChange()
    {
        KeyInput keyInput = new KeyInput();

        for (int i = 0; i < keyText.Count; i++)
        {
            keyInput = KeyInput.LEFT + i;
            keyText[i].text = KeyInputKoreans[i];
            buttonText[i].text = KeyDiction.keys[(KeyInput)i].ToString();
        }
    }

    private string KeyInputKorean(KeyInput input)
    {
        string a = "a";

        return a;
    }
}
