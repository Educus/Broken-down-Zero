using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    void Start()
    {
        if (transform.childCount == 0) return;

        float posZ = 0;
        Debug.Log("Dd");
        foreach (Transform child in transform)
        {
            child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, posZ);
            posZ += 0.01f;
        }
    }
}
