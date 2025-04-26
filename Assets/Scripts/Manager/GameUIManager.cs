using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField] Canvas UIcanvas;
    [Header ("Player UI")]
    [SerializeField] Slider sliderHp;
    [SerializeField] Image dashImage;
    [SerializeField] Image dashCool;
    [SerializeField] Image skill1Image;
    [SerializeField] Image skill1Cool;
    [SerializeField] Image skill2Image;
    [SerializeField] Image skill2Cool;

    private float playerMaxHp;
    private float playerHp;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        PlayerHp();
    }

    private void PlayerHp()
    {
        if (GameManager.Instance.player == null) return;

        Player player = GameManager.Instance.player.GetComponent<Player>();
        playerMaxHp = player.playerMaxHp;
        playerHp = player.playerHp;

        sliderHp.value = playerHp / playerMaxHp;
    }

    public IEnumerator DashCoolTime(float time)
    {
        float collTime = time;

        while (time > 0)
        {
            time -= Time.deltaTime;

            dashCool.fillAmount = time / collTime;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator Skill1CoolTime(float time)
    {
        float collTime = time;

        while (time > 0)
        {
            time -= collTime;

            dashCool.fillAmount = time / collTime;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator Skill2CoolTime(float time)
    {
        float collTime = time;

        while (time > 0)
        {
            time -= collTime;

            dashCool.fillAmount = time / collTime;

            yield return new WaitForFixedUpdate();
        }
    }

}
