using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchZone : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.FindTarget(collision.gameObject);
        }
    }
}
