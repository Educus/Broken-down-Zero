using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour, IInteraction
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InteractionManager.Instance.interaction != null) return;

        if (collision.tag == "Player")
            InteractionManager.Instance.interaction = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            InteractionManager.Instance.interaction = null;
    }

    public abstract void Interact();
}
