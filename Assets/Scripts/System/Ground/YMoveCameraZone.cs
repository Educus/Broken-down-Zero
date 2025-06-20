using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMoveCameraZone : MonoBehaviour
{
    [SerializeField] DungeonCamera camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            camera.yMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            camera.yMove = false;
        }
    }
}
