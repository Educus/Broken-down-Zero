using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyInput
{
    LEFT, 
    RIGHT,
    DOWN,
    ATTACK,
    JUMP,
    DASH,
    SKILL1,
    SKILL2,
    INVENTORY,
    INTERACTION,
    KEYCOUNT
}
public static class KeyDiction
{
    public static Dictionary<KeyInput, KeyCode> keys = new Dictionary<KeyInput, KeyCode>();
}

public class KeyManager : Singleton<KeyManager>
{
    Player player;

    KeyCode[] defaultKeys = new KeyCode[]
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.X,
        KeyCode.C,
        KeyCode.Z,
        KeyCode.A,
        KeyCode.S,
        KeyCode.E,
        KeyCode.F
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
        if (!GameManager.Instance.isPlaying)
        {
            IsNotPlay();
            return;
        }
        if (GameManager.Instance.player == null) return;
        else { player = GameManager.Instance.player.GetComponent<Player>(); }

        InputKey();
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

    private void InputKey()
    {
        if (Input.GetKey(KeyDiction.keys[KeyInput.LEFT]))
        {
            player.left = true;
        }
        if (Input.GetKeyUp(KeyDiction.keys[KeyInput.LEFT]))
        {
            player.left = false;
        }
        if (Input.GetKey(KeyDiction.keys[KeyInput.RIGHT]))
        {
            player.right = true;
        }
        if (Input.GetKeyUp(KeyDiction.keys[KeyInput.RIGHT]))
        {
            player.right = false;
        }
        if (Input.GetKey(KeyDiction.keys[KeyInput.DOWN]))
        {
            player.down = true;
        }
        if (Input.GetKeyUp(KeyDiction.keys[KeyInput.DOWN]))
        {
            player.down = false;
        }



        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.ATTACK])) 
        {
            player.Attack();
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.JUMP]))
        {
            player.Jump();
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.DASH]))
        {
            player.Dash();
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.SKILL1]))
        {
            player.Skill1();
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.SKILL2]))
        {
            player.Skill2();
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.INVENTORY]))
        {
            InventoryManager.Instance.OpenInven();
        }
        if (Input.GetKeyDown(KeyDiction.keys[KeyInput.INTERACTION]))
        {
            if (InteractionManager.Instance.interaction == null) return;

            InteractionManager.Instance.interaction.Interact();
        }

    }

    private void IsNotPlay()
    {
        if (player == null) return;

        player.left = false;
        player.right = false;
        player.down = false;
    }

    public void KeyReset()
    {
        KeyCode[] defaultKeys = new KeyCode[]
        {
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.DownArrow,
            KeyCode.X,
            KeyCode.C,
            KeyCode.Z,
            KeyCode.A,
            KeyCode.S,
            KeyCode.E,
            KeyCode.F
        };

        KeyDiction.keys.Clear();
        for (int i = 0; i < (int)KeyInput.KEYCOUNT; i++)
        {
            KeyDiction.keys.Add((KeyInput)i, defaultKeys[i]);
        }
    }
}
