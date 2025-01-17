using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeySettingText : MonoBehaviour
{
    [SerializeField] public TMP_Text[] buttonText;

    void Start()
    {
        KeyTextChange();
    }

    void Update()
    {
        KeyTextChange();
    }

    private void KeyTextChange()
    {
        for (int i = 0; i < buttonText.Length - 1; i++)
        {
            buttonText[i].text = KeyDiction.keys[(KeyInput)i].ToString();
        }
    }
}
