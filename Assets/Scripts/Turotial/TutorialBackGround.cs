using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBackGround : MonoBehaviour
{
    private Transform camera;

    private Vector3 cameraStartPos;
    private float distance;

    [SerializeField] private Material materials;

    [Header("Move Speed")]
    [Tooltip("배경의 수만큼 생성, 맨 뒤의 배경부터 속도 조정")]
    [SerializeField][Range(0.01f, 1.0f)] private float parallaxSpeed;

    private void Awake()
    {
        camera = GameObject.Find("Main Camera (1)").transform;
        cameraStartPos = camera.position;

        GameObject backGround = new GameObject();
        backGround = transform.GetChild(0).gameObject;
        materials = backGround.GetComponent<Renderer>().material;
    }

    void LateUpdate()
    {
        distance = camera.position.x - cameraStartPos.x;
        transform.position = new Vector3(camera.position.x, 0, 0);
        materials.SetTextureOffset("_MainTex", new Vector2(distance, 0) * parallaxSpeed / 10);

    }
}
