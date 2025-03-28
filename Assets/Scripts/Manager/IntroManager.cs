using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject[] introBackground;
    [SerializeField] TMP_Text monologueText;
    [SerializeField] GameObject pushKeyImage;

    private int backGroundNum;
    private int num = 0;

    [HideInInspector] private bool actionIntro = true;
    private bool isCoroutineRunning = false;

    void Start()
    {
        num = 0;

        foreach (GameObject backGround in introBackground)
        {
            backGround.SetActive(false);
        }
        introBackground[0].SetActive(true);
        backGroundNum = 0;

        monologueText.text = "";
        pushKeyImage.SetActive(false);

        StartCoroutine(IIntroText(textIntroList[num + 2]));
    }

    void Update()
    {
        if (!actionIntro) return;

        if(Input.GetKeyDown(KeyCode.F))
        {
            ChangeBackGround();
            TypingText();
        }
    }


    private void ChangeBackGround()
    {
        int backNum = int.Parse(textIntroList[num]);

        if(backGroundNum != backNum)
        {
            introBackground[backGroundNum].SetActive(false);
            introBackground[backNum].SetActive(true);

            backGroundNum = backNum;

        }
    }

    private void TypingText()
    {
        string textIntro = textIntroList[num + 2];

        if (isCoroutineRunning)
        {
            StopCoroutine(StartCoroutine(IIntroText(textIntro)));
            isCoroutineRunning = false;

            monologueText.text = textIntro;
            if (textIntro != "") pushKeyImage.SetActive(true);
        }
        else
        {
            StartCoroutine(IIntroText(textIntro));
            pushKeyImage.SetActive(false);
        }
    }

    IEnumerator IIntroText(string value)
    {
        isCoroutineRunning = true;

        monologueText.text = "";
        foreach (char c in value) 
        {
            monologueText.text += c;
            yield return new WaitForSeconds(0.1f);

            if (!isCoroutineRunning) break;
        }

        yield return null;

        num += 3;
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
            "6", "0", "���� �����ϴ� �� ���� ���ߴ� ��� �� ���ڱ�\n�����ϴ� ������ �ò����� �Ҹ��� �Բ� �� ���� ������������ �� ���� �Ǿ���."
        };




}
