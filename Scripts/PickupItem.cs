using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickUp;

    void Update()
    {
       if (canPickUp && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().itemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
        }

    }
}
