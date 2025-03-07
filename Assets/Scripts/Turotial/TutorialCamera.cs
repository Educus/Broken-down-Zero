using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    private Transform target;
    private Transform startPos;
    private float cameraSpeed = 5f;
    [SerializeField] public Vector3 minMove;
    [SerializeField] public Vector3 maxMove;

    void Start()
    {
        startPos = transform;
        target = GameManager.Instance.player.transform;
    }

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, -10);
        targetPos.x = Mathf.Clamp(targetPos.x, minMove.x, maxMove.x);

        transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
    }
}
