using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEndEvent : MonoBehaviour
{
    [SerializeField] TMP_Text boss_Text;
    [SerializeField] Image endImage;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    private Player player;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameManager.Instance.player.GetComponent<Player>();

        StartCoroutine(IEEndEvent());
    }
    
    void Update()
    {
        anim.SetInteger("VelocityX", (int)rigid.velocity.x);
    }

    private void Move(float value)
    {
        rigid.velocity = new Vector2(sprite.flipX ? value : -value, 0f);
    }
    private void LookPlayer()
    {
        sprite.flipX = transform.position.x - player.transform.position.x >= 0 ? false : true;
    }

    private IEnumerator IEEndEvent()
    {
        LookPlayer();

        while (Mathf.Abs(transform.position.x - player.transform.position.x) > 5f) // 플레이어와의 거리가 *이상일 때 움직임
        {
            Move(2f);

            yield return null;
        }

        Move(0f);

        yield return StartCoroutine(BossText());

        Move(1.5f);

        StartCoroutine(EndEvent());
    }
    
    // 보스 대사
    private IEnumerator BossText()
    {
        // "탈출한 프로토 타입 실험체..."
        // "실패작... 가치.. 없음.."

        boss_Text.text = "";
        yield return new WaitForSeconds(1.5f);

        foreach (char c in "탈출한 프로토 타입 실험체...")
        {
            boss_Text.text += c;
            yield return new WaitForSeconds(0.075f);
        }

        yield return new WaitForSeconds(1.5f);

        boss_Text.text = "";

        foreach (char c in "실패작...  가치..  없음..")
        {
            boss_Text.text += c;
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1.5f);

        boss_Text.text = "";
    }

    // 서서히 어두워지고 마을로 이동
    private IEnumerator EndEvent()
    {
        endImage.gameObject.SetActive(true);

        Color color = endImage.color;

        color.a = 0;

        while (color.a <= 1.0f)
        {
            color.a += Time.deltaTime * 1.5f;

            endImage.color = color;

            yield return null;
        }

        StartCoroutine(SceneController.Instance.AsyncLoad(3));
        player.Recovery();
    }
}
