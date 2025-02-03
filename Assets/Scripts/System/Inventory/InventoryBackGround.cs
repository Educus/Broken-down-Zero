using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBackGround : MonoBehaviour
{
    [SerializeField] private Image inventoryOutSpace;

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        RectTransform rectTransform = GetComponent<RectTransform>();

        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos))
        {
            inventoryOutSpace.raycastTarget = false;
        }
        else
        {
            inventoryOutSpace.raycastTarget = true;
        }
    }
}
