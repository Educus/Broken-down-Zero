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
        
        // textIntro�� ""�϶��� ȭ�� ��ȯ
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
            // �ؽ�Ʈ�� ��� �� �� ��
            // ����ϴ� �ڷ�ƾ�� ���߰�, ��� ���
            StopCoroutine(StartCoroutine(IIntroText(textIntro)));
            isCoroutineRunning = false;

            monologueText.text = textIntro;
            if (textIntro != "") pushKeyImage.SetActive(true);
        }
        else
        {
            // �ؽ�Ʈ�� ��� ���� �ƴ� ��
            // 0�϶��� ����, 1�϶��� Ư�� �ι� ���
            // Ư�� �ι��� �� ����� ��ġ�� ���ο� �迭��
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
            "0", "0", "���� �ձ����� �Ƿ��� ���� ������ ����̴�.",
            "0", "0", "������ �ӹ��� �����ϴ� ���� ����� ����\n�̰����� �� �� ���� �뿡 ������ �Ǿ���.",
            "1", "1", "",
            "1", "0", "��ġ ���ϱ��� �ձ� ��ó ������ ��ö�� �� ������ü��\n������� ��ġ�Ѵٴ� �ҹ��� �����.",
            "1", "0", "ó������ �����ϴ� ���������� ����������\n����� ���ӿ��� �ֹε��� ���� ������� �ִٴ� �ҽ��� ���ϰ� �ȴ�.",
            "1", "0", "�� �ҽ��� ���� �ղ��� ������\n���ӿ����� ��ġ����� �����϶�� �ӹ��� �����̴�.",
            "2", "1", "",
            "2", "0", "���� �ӹ��� �޾Ƶ鿩 ������ ���߰�\n�ֹε��� ���� ������ٴ� ��� ��ó�� �ִ� ������ ����.",
            "2", "0", "�� �� ������ �����ִ� �������� ������� ��ġ�� �ϰ�\n�ձ� ����鿡�� ���� �ֱ� ���� ������ �۶߸��ٰ� ���� ������...",
            "2", "0", "���� ����� ������ ���ڸ��� ������ �ʿ��Ѱ��� ���� �Ǿ���.",
            "3", "1", "",
            "3", "0", "�� �� ö�ε� �濡�� ���� ���� �̻��� �Ҹ������� ó������ ����� ������.",
            "3", "0", "�� ���� ��ġ ���� �̾߱⿡ ������ ����������Ҵ�.",
            "3", "0", "���� �� ���� �����ϴ� ���� �������� ����� ���ߴ�.",
            "4", "1", "",
            "4", "0", "����� ���� �� �������\n�� ������ �̻��� ��ö�� �ٲ���� �̻��� �뿡 ���� �־���.",
            "4", "0", "���ų� ��ų� ���� �ϴ� �� �ܿ� �ƹ��͵� �� ������ �����̴�.",
            "5", "1", "",
            "5", "0", "���� ��ġ ���㿡 ��޵� ��ö�� �� ������ü�� ���� ���Ҵ�.",
            "5", "0", "�װ͵��� �ߺ� �Ҹ��� ���� ���� ���� �ִ�.",
            "5", "1", "�ߺ�~",
            "5", "1", "�ߺ��",
            "5", "0", "�װ� ���� �� �� ���� �ߺ� �Ÿ��� �Ҹ��� ��ȭ���ϴ� �� ���Ҵ�.",
            "5", "0", "���װ͵��� ��ü�� �����̰� ������ ���� ���� ������ ������� ��ġ�� ���ϱ�?��",
            "5", "0", "���� �������� ä �ð��� ���� ���̾���.",
            "6", "1", "",
            "6", "0", "���� �����ϴ� �� ���� ���ߴ� ��� �� ���ڱ�\n�����ϴ� ������ �ò����� �Ҹ��� �Բ� �� ���� ������ ������ �� ���� �Ǿ���.",
            "7", "1", ""
        };




}
