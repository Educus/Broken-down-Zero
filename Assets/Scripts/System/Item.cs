using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Item : MonoBehaviour
{
    public int itemCode = 10000;
    public int itemNum = 1;

    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EatItem();
            Destroy(gameObject);
        }
    }

    private void EatItem()
    {
        InventoryManager.Instance.GetItem(itemCode, itemNum);
    }

}
