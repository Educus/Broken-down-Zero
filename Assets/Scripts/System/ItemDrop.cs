using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject itemObjectPrefab;
    [SerializeField] private GameObject[] nextText;
    [SerializeField] DBItem[] dropItems;
    
    public void DropItems()
    {
        int length = dropItems.Length;
        float pos = (length - 1) * 0.75f;

        for (int i = 0; i < length; i++)
        {
            if (dropItems[i] == null) return;

            GameObject item = Instantiate(itemObjectPrefab);

            item.transform.position = transform.position + new Vector3((i * 1.5f) - pos, 0, 0);
            item.GetComponent<Item>().indbItem = dropItems[i];
            item.GetComponent<Item>().OnSimulated();
        }

        nextText[0].SetActive(true);
        nextText[1].SetActive(true);
    }
}
