using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] public float speed;

    void Update()
    {
        transform.position = GameManager.Instance.player.transform.position + new Vector3(0, 2, -10);

        return;

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(0.001f,0,0) * speed;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.001f,0,0) * speed;
        }
    }
}
