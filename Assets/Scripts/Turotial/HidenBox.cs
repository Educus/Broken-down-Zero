using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HidenBox : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private float centorPosX = -13;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            // (최대 3) 0에 가까울 수록 타일맵의 투명도가 100(최대 255)
            float distance = Mathf.Max(Mathf.Abs(collision.transform.position.x - centorPosX), 1);
            // 최소치 + ((in현재값 - in최소값) / (in최대값 - in최소값)) * (out최대 - out최소) = 비율값
            float colorA = 100 + ((distance - 1) / (4 - 1)) * (255 - 100);

            tilemap.color = new Color(1,1,1,colorA / 255);
        }
    }
}
