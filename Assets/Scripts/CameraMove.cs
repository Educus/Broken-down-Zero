using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    private Transform startPos;
    private Vector3 offset = new Vector3(0, 2, -10);
    private float cameraSpeed = 5f;

    [SerializeField] public Vector3 minMove;
    [SerializeField] public Vector3 maxMove;

    private void Start()
    {
        target = GameManager.Instance.player.transform;
    }
    void FixedUpdate()
    {
        Vector3 destination = new Vector3(target.position.x, transform.position.y, -10);
        destination.x = Mathf.Clamp(destination.x, minMove.x, maxMove.x);

        transform.position = Vector3.Lerp(transform.position, destination, cameraSpeed * Time.deltaTime);
    }
}
