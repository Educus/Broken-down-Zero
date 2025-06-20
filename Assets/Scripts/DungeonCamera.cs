using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    private Transform target;
    private Vector3 offset = new Vector3(0, 2, -10);
    private float cameraSpeed = 5f;
    [SerializeField] public bool yMove = false;

    [SerializeField] public Vector3 minMove;
    [SerializeField] public Vector3 maxMove;

    private float yMax;
    private float yMin;

    private void Start()
    {
        target = GameManager.Instance.player.transform;
        minMove = startingPoint.position + offset;
        yMax = target.position.y + 2;
    }
    void FixedUpdate()
    {
        float value;

        if (yMove)
        {
            value = target.transform.position.y + 2;
            yMax = float.MaxValue;
            yMin = float.MinValue;
        }
        else
        {
            value = transform.position.y;
            yMax = target.position.y + 2;
            yMin = target.position.y;
        }

        Vector3 destination = new Vector3(target.position.x, value, -10);
        destination.x = Mathf.Clamp(destination.x, minMove.x, maxMove.x);
        destination.y = Mathf.Clamp(destination.y, yMin, yMax);

        transform.position = Vector3.Lerp(transform.position, destination, cameraSpeed * Time.deltaTime);
    }
}
