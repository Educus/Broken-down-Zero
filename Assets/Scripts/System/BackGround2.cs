using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround2 : MonoBehaviour
{
    private Transform camera;

    private Vector3 cameraStartPos;
    private float distance;

    private Material[] materials;

    [Header ("Move Speed")]
    [Tooltip ("배경의 수만큼 생성, 맨 뒤의 배경부터 속도 조정")]
    [SerializeField] [Range(0.01f, 1.0f)] private float[] parallaxSpeed;

    // 현재 설정 값
    // 0.025, 0.05, 0.08, 0.06, 0.08, 0.1, 0.4, 0.5
    // 

    private void Awake()
    {
        camera = GameObject.Find("Main Camera").transform;

        cameraStartPos = camera.position;

        int backgroundCount = transform.childCount;
        GameObject[] backGround = new GameObject[backgroundCount];

        materials = new Material[backgroundCount];
        // layerMoveSpeed = new float[backgroundCount];

        for(int i = 0; i < backgroundCount; i++)
        {
            backGround[i] = transform.GetChild(i).gameObject;
            materials[i] = backGround[i].GetComponent<Renderer>().material;
        }

    }
    
    void LateUpdate()
    {
        distance = camera.position.x - cameraStartPos.x;
        transform.position = new Vector3(camera.position.x, camera.position.y, 0);
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * parallaxSpeed[i] / 10);

            if(i >= 6)
            {
                transform.GetChild(i).gameObject.transform.position = new Vector3(transform.GetChild(i).gameObject.transform.position.x, 6, transform.GetChild(i).gameObject.transform.position.z);
            }
        }
    }
}
