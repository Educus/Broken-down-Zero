using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackZone : MonoBehaviour
{
    private Player player;

    public void HitDamage(float value)
    {
        if (player != null) player.IDamage(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
        }
    }
}
