using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeySettingText : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> keyText;
    [SerializeField] private List<TMP_Text> buttonText;

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
            keyText[i].text = keyInput.ToString();
            buttonText[i].text = KeyDiction.keys[(KeyInput)i].ToString();
        }
    }
}
