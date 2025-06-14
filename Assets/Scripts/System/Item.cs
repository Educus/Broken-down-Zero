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
    
    [SerializeField] private WaitForSeconds inactiveTime = new WaitForSeconds(0.25f);

    private bool isEat = true;
    private bool isData = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().sortingOrder = 12;
        GetComponent<SpriteRenderer>().sprite = mDBItem.ItemImage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

        }

        if (!isEat) return;

        if (collision.tag == "Player")
        {
            EatItem();
        }
    }

    private void EatItem()
    {
        InventoryManager.Instance.GetItem(this);
    }
    public void SetItem(DBItem dbItem, bool isData)
    {
        if (mDBItem == null)
            mDBItem = dbItem;

        this.isData = isData;
    }
    public void DestoyItem()
    {
        if (isData) return;

        Destroy(gameObject);
    }

    public void OnSimulated()
    {
        isEat = false;

        StartCoroutine(IEOnSimulated());
    }

    private IEnumerator IEOnSimulated()
    {
        yield return inactiveTime;

        isEat = true;
    }
}
