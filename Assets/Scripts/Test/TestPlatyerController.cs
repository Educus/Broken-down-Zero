using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private Player player;

    public void Hit()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        player.GetComponent<IHitable>().IDamage(0);
    }
    public void Dead()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        player.GetComponent<IHitable>().IDamage(9999);
    }
    public void Respawn()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        player.Recovery();
    }


}
