using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] playerStats;

    public bool gameMenuOpen = false;
    public bool dialogueActive = false;
    public bool fadingBetweenAreas = false;
    public bool shopActive = false;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
    }


    void Update()
    {
       PlayerController.instance.canMove = !(
            gameMenuOpen || 
            dialogueActive || 
            fadingBetweenAreas || 
            shopActive);


        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }
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
                if (itemsHeld[index] == "" || numberOfItems[index] < 1)
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

    public bool RemoveItem(string itemToRemove)
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
            return false;
        }

        if (GameMenu.instance.gameObject.activeInHierarchy)
        {
            GameMenu.instance.ShowItems();
        }
        return true;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_X", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", PlayerController.instance.transform.position.z);

        // Save character info.

        for (int playerIndex = 0; playerIndex < playerStats.Length; playerIndex++)
        {
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_active", playerStats[playerIndex].gameObject.activeInHierarchy ? 1 : 0);

            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_level", playerStats[playerIndex].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_currentExp", playerStats[playerIndex].currentExperience);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_currentHP", playerStats[playerIndex].currentHealthPoints);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_MaxHP", playerStats[playerIndex].maxHealthPoints);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_CurrentMP", playerStats[playerIndex].currentMagicPoints);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_MaxMP", playerStats[playerIndex].maxMagicPoints);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_Strength", playerStats[playerIndex].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_Defense", playerStats[playerIndex].defense);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_WeaponPower", playerStats[playerIndex].weaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[playerIndex].characterName + "_ArmorPower", playerStats[playerIndex].armorPower);

            PlayerPrefs.SetString("Player_" + playerStats[playerIndex].characterName + "_EquippedWeapon", playerStats[playerIndex].equippedWeapon);
            PlayerPrefs.SetString("Player_" + playerStats[playerIndex].characterName + "_EquippedArmor", playerStats[playerIndex].equippedArmor);
        }

        // Store inventory data.
        for (int inventoryIndex = 0; inventoryIndex < itemsHeld.Length; inventoryIndex++)
        {
            PlayerPrefs.SetString("InventoryItem_" + inventoryIndex + "_name", itemsHeld[inventoryIndex]);
            PlayerPrefs.SetInt("InventoryItem_" + inventoryIndex + "_amount", numberOfItems[inventoryIndex]);
        }
    }

    public void LoadData()
    {
        //SceneManager.SetActiveScene(PlayerPrefs.GetString("Current_Scene"));
        PlayerController.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Position_X"),
            PlayerPrefs.GetFloat("Player_Position_Y"),
            PlayerPrefs.GetFloat("Player_Position_Z")
        );

        // Load character info.
        for (int playerIndex = 0; playerIndex < playerStats.Length; playerIndex++)
        {
            playerStats[playerIndex].gameObject.SetActive(PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_active") == 1);

            playerStats[playerIndex].playerLevel            = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_level");
            playerStats[playerIndex].currentExperience      = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_currentExp");
            playerStats[playerIndex].currentHealthPoints    = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_currentHP");
            playerStats[playerIndex].maxHealthPoints        = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_MaxHP");
            playerStats[playerIndex].currentMagicPoints     = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_CurrentMP");
            playerStats[playerIndex].maxMagicPoints         = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_MaxMP");
            playerStats[playerIndex].strength               = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_Strength");
            playerStats[playerIndex].defense                = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_Defense");
            playerStats[playerIndex].weaponPower            = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_WeaponPower");
            playerStats[playerIndex].armorPower             = PlayerPrefs.GetInt("Player_" + playerStats[playerIndex].characterName + "_ArmorPower");

            playerStats[playerIndex].equippedWeapon         = PlayerPrefs.GetString("Player_" + playerStats[playerIndex].characterName + "_EquippedWeapon");
            playerStats[playerIndex].equippedArmor          = PlayerPrefs.GetString("Player_" + playerStats[playerIndex].characterName + "_EquippedArmor");
        }

        // Load inventory data.
        for (int inventoryIndex = 0; inventoryIndex < itemsHeld.Length; inventoryIndex++)
        {
            itemsHeld[inventoryIndex] = PlayerPrefs.GetString("InventoryItem_" + inventoryIndex + "_name");
            numberOfItems[inventoryIndex] = PlayerPrefs.GetInt("InventoryItem_" + inventoryIndex + "_amount");
        }
    }
}
