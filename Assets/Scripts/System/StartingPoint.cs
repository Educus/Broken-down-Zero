using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.startingPoint = gameObject;
        GameManager.Instance.StartingPlayer();
    }
}
