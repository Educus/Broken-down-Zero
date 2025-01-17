using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyInput
{
    LEFT, 
    RIGHT,
    ATTACK,
    JUMP,
    DASH,
    KEYCOUNT
}
public static class KeyDiction
{
    public static Dictionary<KeyInput, KeyCode> keys = new Dictionary<KeyInput, KeyCode>();
}

public class KeyManager : Singleton<KeyManager>
{
    GameObject player;

    KeyCode[] defaultKeys = new KeyCode[]
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C
    };

    private void Awake()
    {
        KeyDiction.keys.Clear();
        for (int i = 0; i < (int)KeyInput.KEYCOUNT; i++)
        {
            KeyDiction.keys.Add((KeyInput)i, defaultKeys[i]);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.player == null) return;
        else { player = GameManager.Instance.player; }

        TestInput();
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;

        if (keyEvent.isKey)
        {
            if (keyEvent.keyCode != KeyCode.Escape)
            {
                KeyDiction.keys[(KeyInput)key] = keyEvent.keyCode;
            }
            key = -1;
        }
    }
    int key = -1;

    public void ChangeKey(int num)
    {
        key = num;
    }

    private void TestInput()
    {
        if (Input.GetKey(KeyDiction.keys[KeyInput.LEFT]))
        { 
            player.GetComponent<Player>().left = true;
        }
        if (Input.GetKeyUp(KeyDiction.keys[KeyInput.LEFT]))
        {
            player.GetComponent<Player>().left = false;
        }
        if (Input.GetKey(KeyDiction.keys[KeyInput.RIGHT]))
        {
            player.GetComponent<Player>().right = true;
        }
        if (Input.GetKeyUp(KeyDiction.keys[KeyInput.RIGHT]))
        {
            player.GetComponent<Player>().right = false;
        }


        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.ATTACK])) 
        {
            player.GetComponent<Player>().attack = true;
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.JUMP]))
        { 
            player.GetComponent<Player>().jump = true;
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.DASH]))
        {
            player.GetComponent<Player>().dash = true;
        }
    }
}
