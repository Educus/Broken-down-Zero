using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject playingTutorial;

    private void Start()
    {
        playingTutorial.SetActive(false);
    }
    public void StartButton()
    {
        playingTutorial.SetActive(true);
    }
    public void PlayingTutorial(bool play)
    {
        if (play)
        {
            GameManager.Instance.NewPlayer();
            GameManager.Instance.MovePlayer();
            StartCoroutine(SceneController.Instance.AsyncLoad(1));
        }
        else
        {
            GameManager.Instance.LoadPlayer();
            GameManager.Instance.MovePlayer();
            StartCoroutine(SceneController.Instance.AsyncLoad(2));
        }

    }
    public void SettingButton()
    {
        Setting.Instance.openSetting();
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
