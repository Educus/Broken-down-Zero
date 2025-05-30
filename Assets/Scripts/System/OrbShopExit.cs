using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShopExit : MonoBehaviour
{
    [SerializeField] OrbShop shop;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shop.CloseShop();
        }
    }
}
