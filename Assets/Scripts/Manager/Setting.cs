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

    public void Button1() // 버튼1
    {

    }
    public void ButtonSetting() // 키세팅, 볼륨조절
    {
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }
    public void ButtonExit() // 나가기?
    {

    }
}
