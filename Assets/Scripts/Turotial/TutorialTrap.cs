using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrap : MonoBehaviour
{
    [SerializeField] private GameObject respawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<IHitable>().IDamage(999);
        }
    }
}
