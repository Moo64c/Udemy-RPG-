using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyWindow;
    public GameObject sellWindow;

    public Text goldAmountText;

    // Buy window.
    public ItemButton[] buyItemButtons;
    public Text buyItemName, buyItemDescription, buyItemValue;

    // Sell window.
    public ItemButton[] sellItemButtons;
    public Text sellItemName, sellItemDescription, sellItemValue;

    public string[] itemsForSale;
    public Item selectedItem;

    private void Start()
    {
        instance = this;
    }

    private void UpdateGold()
    {
        goldAmountText.text = GameManager.instance.currentGold + "g";

    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        GameManager.instance.shopActive = true;
        OpenBuyMenu();
        UpdateGold();
    }


    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyWindow.SetActive(true);
        sellWindow.SetActive(false);
        buyItemButtons[0].Press();

        for (int index = 0; index < buyItemButtons.Length; index++)
        {
            buyItemButtons[index].buttonValue = index;

            if (itemsForSale[index] != "")
            {
                buyItemButtons[index].buttonImage.gameObject.SetActive(true);
                buyItemButtons[index].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[index]).itemSprite;
                buyItemButtons[index].amountText.text = "";
            }
            else
            {
                buyItemButtons[index].buttonImage.gameObject.SetActive(false);
                buyItemButtons[index].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {

        buyWindow.SetActive(false);
        sellWindow.SetActive(true);
        sellItemButtons[0].Press();
        ShowSellItems();
    }


    private void ShowSellItems()
    {
        GameManager.instance.SortItems();
        for (int index = 0; index < sellItemButtons.Length; index++)
        {
            sellItemButtons[index].buttonValue = index;

            if (GameManager.instance.itemsHeld[index] != "")
            {
                sellItemButtons[index].buttonImage.gameObject.SetActive(true);
                sellItemButtons[index].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[index]).itemSprite;
                sellItemButtons[index].amountText.text = GameManager.instance.numberOfItems[index].ToString();
            }
            else
            {
                sellItemButtons[index].buttonImage.gameObject.SetActive(false);
                sellItemButtons[index].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        UpdateGold();
    }

    public void SelectSellItem(Item sellItem)
    {
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.description;
        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * 0.5f) + "g";
    }

    public void BuyItem()
    {
        if (GameManager.instance.currentGold >= selectedItem.value)
        {
            GameManager.instance.currentGold -= selectedItem.value;
            GameManager.instance.AddItem(selectedItem.itemName);
        }

        UpdateGold();
    }

    public void SellItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.RemoveItem(selectedItem.itemName))
            {
                GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * 0.5f);
            }
        }
        UpdateGold();
        ShowSellItems();
    }
}
