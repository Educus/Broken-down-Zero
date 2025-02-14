using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 2, -10);
    [SerializeField] public float speed;

    private void Start()
    {
        target = GameManager.Instance.player.transform;
    }
    void LateUpdate()
    {

        Vector3 destination = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
    }

}
