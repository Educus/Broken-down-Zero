using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillTwist : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    private SpriteRenderer sprite;
    private float scaleX;

    void Start()
    {
        sprite = Enemy.GetComponent<SpriteRenderer>();
        scaleX = transform.localScale.x;
    }

    void Update()
    {
        transform.localScale = new Vector2(sprite.flipX ? -scaleX : scaleX, transform.localScale.y);
    }
}
