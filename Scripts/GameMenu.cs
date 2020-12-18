using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject theMenu;

    public GameObject[] windows;

    // Main window
    [Header("Main Window")]
    public Text[] nameText = new Text[3];
    public Text[] healthPointsText = new Text[3];
    public Text[] magicPointsText = new Text[3];
    public Text[] levelText = new Text[3];
    public Text[] experienceText = new Text[3];
    public Slider[] experienceSliders = new Slider[3];
    public Image[] characterImage = new Image[3];
    public GameObject[] characterStatHolder = new GameObject[3];

    // Status window.
    [Header("Status Window")]
    public GameObject[] statusButtons;
    public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWeaponEquipped, statusWeaponPower, statusArmorEquipped, statusArmorPower, statusExperience;
    public Image statusImage;

    // Item window.
    [Header("Item Window")]
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;
    public GameObject itemCharacterSelectionPanel;
    public Text[] itemCharacterSelectNames;

    private CharacterStats[] playerStats;

    void Start()
    {
        instance = this;

        CloseMenu();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            UpdateMainStats();
            if (theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                GameManager.instance.gameMenuOpen = true;
                theMenu.SetActive(true);
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;
        for(int index = 0; index < playerStats.Length; index++) 
        {
            if (playerStats[index].gameObject.activeInHierarchy)
            {
                var playerStat = playerStats[index];
                nameText[index].text = playerStat.characterName;
                healthPointsText[index].text        = "HP: " + playerStat.currentHealthPoints   + "/" + playerStat.maxHealthPoints;
                magicPointsText[index].text         = "MP: " + playerStat.currentMagicPoints    + "/" + playerStat.maxMagicPoints;
                levelText[index].text               = "Level: " + playerStat.playerLevel;

                experienceText[index].text          = playerStat.currentExperience + "/" + playerStat.experienceToNextLevel[playerStat.playerLevel - 1];
                experienceSliders[index].maxValue = playerStat.experienceToNextLevel[playerStat.playerLevel - 1];
                experienceSliders[index].value = playerStat.currentExperience;

                characterImage[index].sprite = playerStat.characterImage;
            }
            else
            {
                characterStatHolder[index].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        for (int index = 0; index < windows.Length; index++)
        {
            if (windowNumber == index)
            {
                windows[windowNumber].SetActive(!windows[windowNumber].activeInHierarchy);
            }
            else
            {
                windows[index].SetActive(false);
            }
        }
        itemCharacterSelectionPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int index = 0; index < windows.Length; index++)
        {
            windows[index].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        itemCharacterSelectionPanel.SetActive(false);
    }

    public void OpenStatus()
    {
        // Update the infromation that is shown.
        StatusCharacter(0);

        for (int index = 0; index < statusButtons.Length; index++)
        {
            var playerStat = playerStats[index];
            var statusButton = statusButtons[index];
            statusButton.SetActive(playerStat.gameObject.activeInHierarchy);
            statusButton.GetComponentInChildren<Text>().text = playerStat.characterName;
        }
    }

    public void StatusCharacter(int selected)
    {
        var playerStat = playerStats[selected];

        statusName.text = playerStat.characterName;
        statusHP.text = playerStat.currentHealthPoints  + " / " + playerStat.maxHealthPoints;
        statusMP.text = playerStat.currentMagicPoints   + " / " + playerStat.maxMagicPoints;
        statusStr.text = playerStat.strength.ToString();
        statusDef.text = playerStat.defense.ToString();

        statusWeaponPower.text  = playerStat.weaponPower.ToString();
        statusArmorPower.text   = playerStat.armorPower.ToString();
        statusWeaponEquipped.text   = playerStat.equippedWeapon != "" ? "-" : playerStat.equippedWeapon;
        statusArmorEquipped.text    = playerStat.equippedArmor  != "" ? "-" : playerStat.equippedArmor;

        statusExperience.text = (playerStat.experienceToNextLevel[playerStat.playerLevel - 1] - playerStat.currentExperience).ToString();

        statusImage.sprite = playerStat.characterImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();
        for (int index = 0; index < itemButtons.Length; index++)
        {
            itemButtons[index].buttonValue = index;

            if (GameManager.instance.itemsHeld[index] != "")
            {
                itemButtons[index].buttonImage.gameObject.SetActive(true);
                itemButtons[index].buttonImage.sprite   = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[index]).itemSprite;
                itemButtons[index].amountText.text      = GameManager.instance.numberOfItems[index].ToString();
            }
            else
            {
                itemButtons[index].buttonImage.gameObject.SetActive(false);
                itemButtons[index].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item item)
    {
        activeItem = item;
        useButtonText.text = (item.isItem) ? "Use" : "Equip";

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenCharacterChoice()
    {
        for (int index = 0; index < itemCharacterSelectNames.Length; index++)
        {
            itemCharacterSelectNames[index].text = playerStats[index].characterName;
            itemCharacterSelectNames[index].transform.parent.gameObject.SetActive(playerStats[index].gameObject.activeInHierarchy);
        }
        itemCharacterSelectionPanel.SetActive(true);
    }

    public void CloseCharacterChoice()
    {
        itemCharacterSelectionPanel.SetActive(false);
    }
}
