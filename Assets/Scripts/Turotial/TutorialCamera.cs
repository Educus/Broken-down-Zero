using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    private Transform target;
    private Transform startPos;
    [SerializeField] private Vector3 maxMove;

    void Start()
    {
        startPos = transform;
        target = GameManager.Instance.player.transform;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, -10);
    }
}
