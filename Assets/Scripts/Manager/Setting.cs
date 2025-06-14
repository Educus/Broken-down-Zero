using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : Singleton<Setting>
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject settingCanvas;
    [SerializeField] TMP_Text exitText;

    private int buildIndex;

    private void Start()
    {
        menu.SetActive(false);
        closeCanvas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                closeCanvas();
            }
            else
            {
                menu.SetActive(true);
                menuCanvas.SetActive(true);
            }
        }

        buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex == 0)
        {
            exitText.text = "끝내기";
        }
        else if (buildIndex <= 3 || buildIndex == 6)
        {
            exitText.text = "타이틀로";
        }
        else
        {
            exitText.text = "거점으로";
        }


        if (menu.activeSelf)
            GameManager.Instance.isPlaying = false;
        else
            GameManager.Instance.isPlaying = true;
    }
    public void openSetting()
    {
        menu.SetActive(true);
        settingCanvas.SetActive(true);
    }
    private void closeCanvas()
    {
        menuCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        menu.SetActive(false);
    }

    public void Button1() // 버튼1
    {
        menu.SetActive(false);
        closeCanvas();
    }
    public void ButtonSetting() // 키세팅, 볼륨조절
    {
        openSetting();
    }
    public void ButtonExit() // 나가기?
    {
        if (buildIndex == 0)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        else if(buildIndex <= 3 || buildIndex == 6) // 튜토리얼 ~ 마을
        {
            StartCoroutine(SceneController.Instance.AsyncLoad(0));
            GameManager.Instance.MovePlayer();
        }
        else // 던전입구 ~ 던전
        {
            StartCoroutine(SceneController.Instance.AsyncLoad(3));
            InventoryManager.Instance.InventoryCalculate();
            GameManager.Instance.MovePlayer();
        }
        closeCanvas();
    }
}
