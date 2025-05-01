using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SingleCollisionDamage : MonoBehaviour
{
    [HideInInspector] public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (damage != 0) collision.GetComponent<Player>().IDamage(damage);
        }
    }
}
