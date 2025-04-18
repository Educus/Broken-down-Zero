using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IHitable
{
    // 피해 받았을 때 파괴되어 사라지는 오브젝트
    // 몇번 피해를 받을것인지?
    private SpriteRenderer spriteRender;
    [SerializeField] private GameObject startingPoint;

    private int hitNum = 2;
    private bool isHitting = false;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void IDamage(float damage)
    {
        Destructible();
    }

    private void Destructible()
    {
        if (isHitting) return;

        StartCoroutine(IEHitObject());
    }

    private IEnumerator IEHitObject()
    {
        isHitting = true;

        if (hitNum == 0)
        {
            yield return StartCoroutine(IEDestroyObject());
        }
        else
        {
            hitNum--;
            yield return StartCoroutine(IEHittingObject());
        }

        isHitting = false;

        yield return null;
    }

    private IEnumerator IEHittingObject()
    {
        Color color = spriteRender.color;

        color.g = 1;
        color.b = 1;

        while (color.g >= 0.6f)
        {
            color.g -= Time.deltaTime * 2;
            color.b -= Time.deltaTime * 2;

            spriteRender.color = color;

            yield return null;
        }

        spriteRender.color = new Color(1, 1, 1);

        yield return null;
    }
    private IEnumerator IEDestroyObject()
    {
        Color color = spriteRender.color;

        color.a = 1;

        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;

            spriteRender.color = color;

            yield return null;
        }
        
        gameObject.SetActive(false);

        GameManager.Instance.startingPoint = startingPoint;

        yield return null;

    }
}
