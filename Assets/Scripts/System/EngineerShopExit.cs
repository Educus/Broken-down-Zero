using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerShopExit : MonoBehaviour
{
    [SerializeField] PlayerUpgrade upgrade;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            upgrade.CloseShop();
        }
    }
}
