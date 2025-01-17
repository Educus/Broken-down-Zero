using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : Singleton<Setting>
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settingCanvas;

    private void Start()
    {
        menu.SetActive(false);
        closeCanvas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingCanvas.activeSelf)
            {
                closeCanvas();
            }
            else
            {
                menu.SetActive(!menu.activeSelf);
            }
        }

        if (menu.activeSelf)
            GameManager.Instance.isPlaying = false;
        else
            GameManager.Instance.isPlaying = true;
    }
    private void closeCanvas()
    {
        settingCanvas.SetActive(false);
    }

    public void Button1() // ��ư1
    {

    }
    public void ButtonSetting() // Ű����, ��������
    {
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }
    public void ButtonExit() // ������?
    {

    }
}
