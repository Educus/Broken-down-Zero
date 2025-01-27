using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround1 : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;

    private Vector3 cameraStartPos;
    private float distance;

    private Material[] materials;
    private float[] layerMoveSpeed;

    [SerializeField] [Range(0.01f, 1.0f)] private float parallaxSpeed;

    private void Awake()
    {
        cameraStartPos = cameraTransform.position;

        int backgroundCount = transform.childCount;
        GameObject[] backGround = new GameObject[backgroundCount];

        materials = new Material[backgroundCount];
        layerMoveSpeed = new float[backgroundCount];

        for(int i = 0; i < backgroundCount; i++)
        {
            backGround[i] = transform.GetChild(i).gameObject;
            materials[i] = backGround[i].GetComponent<Renderer>().material;
        }
        BackGroundMove(backGround, backgroundCount);
    }

    private void BackGroundMove(GameObject[] backGround, int count)
    {
        float value = 0;

        for (int i = 0; i < count; i++)
        {
            if ((backGround[i].transform.position.z - cameraTransform.position.z) > value)
            {
                value = backGround[i].transform.position.z - cameraTransform.position.z;
            }
        }

        for (int i = 0; i < count; i++)
        {
            layerMoveSpeed[i] = 1 - (backGround[i].transform.position.z - cameraTransform.position.z) / value;
        }

    }
    void LateUpdate()
    {
        distance = cameraTransform.position.x - cameraStartPos.x;
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, 0);
        for(int i = 0; i < materials.Length; i++)
        {
            float speed = layerMoveSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
