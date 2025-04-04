using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject[] introBackground;
    [SerializeField] GameObject[] targetText;
    [SerializeField] GameObject textBackGround;
    [SerializeField] TMP_Text monologueText;
    [SerializeField] GameObject pushKeyImage;
    [SerializeField] Image changeBackGround;

    private int backGroundNum;
    private int introNum = 0;
    private int targetNum = 0;

    [HideInInspector] private bool actionIntro = true;
    private bool isCoroutineRunning = false;
    private bool isChangeBackGround = false;

    private bool isPlay = true;

    void Start()
    {
        introNum = 0;

        foreach (GameObject backGround in introBackground)
        {
            backGround.SetActive(false);
        }
        introBackground[0].SetActive(true);
        backGroundNum = 0;

        monologueText.text = "";
        pushKeyImage.SetActive(false);
        changeBackGround.gameObject.SetActive(false);

        TypingText();
    }

    void Update()
    {
        if (!actionIntro) return;

        if(Input.GetKeyDown(KeyCode.F))
        {
            TypingText();
        }
    }


    private void ChangeBackGround()
    {
        int backNum = int.Parse(textIntroList[introNum]);

        if(backGroundNum != backNum)
        {
            introBackground[backGroundNum].SetActive(false);
            introBackground[backNum].SetActive(true);

            backGroundNum = backNum;
        }
    }


    private void TypingText()
    {
        if (!isPlay) return;

        if (introNum >= textIntroList.Length)
        {
            isPlay = false;
            StartCoroutine(INextScene());
        }

        string textIntro = textIntroList[introNum + 2];
        
        // textIntro가 ""일때는 화면 전환
        if (textIntro == "")
        {
            if (isChangeBackGround) return;

            monologueText.text = "";
            textBackGround.SetActive(false);
            pushKeyImage.SetActive(false);

            StartCoroutine(IChangeBackGround());
            return;
        }

        if (isCoroutineRunning)
        {
            // 텍스트가 출력 중 일 때
            // 출력하는 코루틴을 멈추고, 모두 출력
            StopCoroutine(StartCoroutine(IIntroText(textIntro)));
            isCoroutineRunning = false;

            monologueText.text = textIntro;
            if (textIntro != "") pushKeyImage.SetActive(true);
        }
        else
        {
            // 텍스트가 출력 중이 아닐 때
            // 0일때는 독백, 1일때는 특정 인물 대사
            // 특정 인물일 때 대사의 위치는 새로운 배열로
            if (textIntroList[introNum + 1] == "0")
            {
                textBackGround.SetActive(true);
                monologueText.rectTransform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                monologueText.fontSize = 40;
            }
            else if (textIntroList[introNum + 1] == "1")
            {
                textBackGround.SetActive(false);
                monologueText.rectTransform.position = targetText[targetNum].transform.position;
                targetNum++;
                monologueText.fontSize = 30;
            }
            else return;

            pushKeyImage.SetActive(false);
            StartCoroutine(IIntroText(textIntro));
        }
    }

    IEnumerator INextScene()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(SceneController.Instance.AsyncLoad(2));
    }
    IEnumerator IChangeBackGround()
    {
        isChangeBackGround = true;

        yield return StartCoroutine(FadeIn());

        isChangeBackGround = false;
        ChangeBackGround();
        introNum += 3;

        yield return StartCoroutine(FadeOut());

        TypingText();
    }
    IEnumerator FadeIn()
    {
        changeBackGround.gameObject.SetActive(true);

        Color color = changeBackGround.color;

        color.a = 0;

        while (color.a <= 1.0f)
        {
            color.a += Time.deltaTime;

            changeBackGround.color = color;

            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        Color color = changeBackGround.color;

        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;

            changeBackGround.color = color;

            yield return null;
        }

        changeBackGround.gameObject.SetActive(false);
    }

    IEnumerator IIntroText(string value)
    {
        isCoroutineRunning = true;

        monologueText.text = "";
        foreach (char c in value) 
        {
            monologueText.text += c;
            yield return new WaitForSeconds(0.05f);

            if (!isCoroutineRunning) break;
        }

        yield return null;

        introNum += 3;
        isCoroutineRunning = false;
        if (value != "") pushKeyImage.SetActive(true);
    }


    private string[] textIntroList = 
        {
            "0", "0", "나는 왕국에서 실력이 좋은 유능한 기사이다.",
            "0", "0", "하지만 임무를 수행하던 도중 기습을 당해\n이곳에서 알 수 없는 통에 갇히게 되었다.",
            "1", "1", "",
            "1", "0", "납치 당하기전 왕국 근처 숲에서 강철로 된 괴생물체가\n사람들을 납치한다는 소문을 들었다.",
            "1", "0", "처음에는 유행하는 괴담정도로 생각했으나\n어느날 숲속에서 주민들이 점점 사라지고 있다는 소식을 접하게 된다.",
            "1", "0", "이 소식을 들은 왕께서 나에게\n숲속에서의 납치사건을 조사하라는 임무를 내리셨다.",
            "2", "1", "",
            "2", "0", "나는 임무를 받아들여 숲으로 향했고\n주민들이 자주 사라진다는 장소 근처에 있는 동굴로 들어갔다.",
            "2", "0", "그 땐 동굴에 숨어있는 도적떼가 사람들을 납치를 하고\n왕국 사람들에게 겁을 주기 위해 괴담을 퍼뜨린다고 생각 했지만...",
            "2", "0", "동굴 깊숙한 곳으로 들어가자마자 생각을 초월한것을 보게 되었다.",
            "3", "1", "",
            "3", "0", "그 곳 철로된 방에는 빛이 나고 이상한 소리를내는 처음보는 통들이 보였다.",
            "3", "0", "이 곳은 마치 옛날 이야기에 나오던 고대유적같았다.",
            "3", "0", "나는 이 곳을 관찰하던 도중 누군가의 기습을 당했다.",
            "4", "1", "",
            "4", "0", "기습을 당한 후 깨어나보니\n내 왼팔이 이상한 강철로 바뀌었고 이상한 통에 갇혀 있었다.",
            "4", "0", "보거나 듣거나 생각 하는 것 외엔 아무것도 할 수없는 상태이다.",
            "5", "1", "",
            "5", "0", "나는 납치 괴담에 언급된 강철로 된 괴생명체를 둘을 보았다.",
            "5", "0", "그것들은 삐빅 소리를 내며 나를 보고 있다.",
            "5", "1", "삐빅~",
            "5", "1", "삐비빅",
            "5", "0", "그것 들은 알 수 없는 삐빅 거리는 소리로 대화를하는 것 같았다.",
            "5", "0", "“그것들은 정체가 무엇이고 무엇을 위해 나를 포함한 사람들을 납치한 것일까?”",
            "5", "0", "같은 생각만한 채 시간을 보낼 뿐이었다.",
            "6", "1", "",
            "6", "0", "나는 생각하는 것 조차 멈추던 어느 날 갑자기\n폭발하는 굉음과 시끄러운 소리와 함께 이 통의 유리가 깨지는 걸 보게 되었다.",
            "7", "1", ""
        };




}
