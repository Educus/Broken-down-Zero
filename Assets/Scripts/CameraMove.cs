using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] public float speed;

    void Update()
    {
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
