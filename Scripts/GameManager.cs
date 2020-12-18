using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] playerStats;

    public bool gameMenuOpen = false;
    public bool dialogueActive = false;
    public bool fadingBetweenAreas = false;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
       PlayerController.instance.canMove = !(gameMenuOpen || dialogueActive || fadingBetweenAreas);
    }

    public Item GetItemDetails(string itemName)
    {
        for (int index = 0; index < referenceItems.Length; index++)
        {
            if (referenceItems[index].itemName == itemName)
            {
                return referenceItems[index];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int index = 0; index < itemsHeld.Length - 1; index++)
            {
                if (itemsHeld[index] == "")
                {
                    itemsHeld[index] = itemsHeld[index + 1];
                    itemsHeld[index + 1] = "";

                    numberOfItems[index] = numberOfItems[index + 1];
                    numberOfItems[index + 1] = 0;

                    if (itemsHeld[index] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int index = 0;

        for (; index < itemsHeld.Length; index++) 
        {
            if (itemsHeld[index] == itemToAdd || itemsHeld[index] == "")
            {
                break;
            }
        }

        if (itemsHeld[index] == "")
        {
            itemsHeld[index] = itemToAdd;
            numberOfItems[index] = 1;

            int referenceItemIndex = 0;
            bool itemExists = false;
            for (; referenceItemIndex < referenceItems.Length; referenceItemIndex++)
            {
                if (referenceItems[referenceItemIndex].itemName == itemToAdd)
                {
                    itemExists = true;
                    break;
                }
            }
            if (!itemExists)
            {
                Debug.LogError("No reference for item " + itemToAdd);
            }
        }
        else if (itemsHeld[index] == itemToAdd)
        {
            numberOfItems[index]++;
        }
        else
        {
            Debug.LogError("no place for new item " + itemToAdd);
        }

        if (GameMenu.instance.gameObject.activeInHierarchy)
        {
            GameMenu.instance.ShowItems();
        }
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (; itemPosition < itemsHeld[itemPosition].Length; itemPosition++)
        {
            if (itemsHeld[itemPosition] == itemToRemove)
            {
                foundItem = true;
                break;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }
        }
        else
        {
            Debug.LogError("Could not remove item, could not find " + itemToRemove);
        }

        if (GameMenu.instance.gameObject.activeInHierarchy)
        {
            GameMenu.instance.ShowItems();
        }
    }
}
