using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitZone : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<IHitable>() == null) return;

            collision.GetComponent<IHitable>().IDamage(enemy.mPower);
        }
    }
}
