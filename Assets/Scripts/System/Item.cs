using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

// public abstract class Item : MonoBehaviour
public class Item : MonoBehaviour
{
    [SerializeField] private DBItem mDBItem;
    public DBItem dbItem { get { return mDBItem; } }
    public DBItem indbItem { set { mDBItem = value; } }
    
    [SerializeField] private WaitForSeconds inactiveTime = new WaitForSeconds(1.5f);

    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().sortingOrder = 7;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EatItem();
        }
    }

    private void EatItem()
    {
        InventoryManager.Instance.GetItem(this);
    }

    public void DestoyItem()
    {
        Destroy(gameObject);
    }

    public void OnSimulated()
    {
        StartCoroutine(IEOnSimulated());
    }

    private IEnumerator IEOnSimulated()
    {
        yield return inactiveTime;

        GetComponent<Rigidbody2D>().simulated = true;
    }
}
