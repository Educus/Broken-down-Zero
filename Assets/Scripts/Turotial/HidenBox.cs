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
            // (�ִ� 3) 0�� ����� ���� Ÿ�ϸ��� ������ 100(�ִ� 255)
            float distance = Mathf.Max(Mathf.Abs(collision.transform.position.x - centorPosX), 1);
            // �ּ�ġ + ((in���簪 - in�ּҰ�) / (in�ִ밪 - in�ּҰ�)) * (out�ִ� - out�ּ�) = ������
            float colorA = 100 + ((distance - 1) / (4 - 1)) * (255 - 100);

            tilemap.color = new Color(1,1,1,colorA / 255);
        }
    }
}
