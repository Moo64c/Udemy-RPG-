using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public string[] ItemsForSale = new string[40];

    private bool canOpen;

    void Update()
    {

        if (canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {

            Shop.instance.itemsForSale = ItemsForSale;

            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
