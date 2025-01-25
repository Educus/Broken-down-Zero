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
            StartCoroutine(SceneController.Instance.AsyncLoad(1));
        else
            StartCoroutine(SceneController.Instance.AsyncLoad(2));

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
