using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject startingPoint;
    [SerializeField] private GameObject prefab;
    [HideInInspector] public bool tutorial = false;

    public bool isPlaying = true;

    public void MovePlayer()
    {
        player.transform.parent = transform;
        isPlaying = false;
        player.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void StartingPlayer()
    {
        player.transform.parent = null;
        player.transform.position = startingPoint.transform.position;
        isPlaying = true;
        player.GetComponent<Rigidbody2D>().simulated = true;

    }

    public void NewPlayer()
    {
        if (player != null) return;

        player = Instantiate(prefab);
        // player = Instantiate(Resources.Load<GameObject>("SaveData/Player"));
        SavePlayer();

    }
    public void LoadPlayer()
    {
        if (player != null) return;

        if (Resources.Load<GameObject>("SaveData/Player"))
        {
            NewPlayer();
        }
        else
        {
            NewPlayer();
        }

    }
    public void SavePlayer()
    {

    }

    public IEnumerator Recovery(int value)
    {
        yield return new WaitForSeconds(value);

        // 플레이어가 튜토리얼에서 죽었을 때
        // 튜토리얼 보스전 및 이외의 곳에서 죽었을 때
        if (tutorial)
        {
            player.GetComponent<Player>().Recovery();
            player.transform.position = startingPoint.transform.position;
        }
        else
        {

        }
    }
}
