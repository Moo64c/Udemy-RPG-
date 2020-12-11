using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public GameObject theMenu;

    public GameObject[] windows;

    public Text[] nameText = new Text[3];
    public Text[] healthPointsText = new Text[3];
    public Text[] magicPointsText = new Text[3];
    public Text[] levelText = new Text[3];
    public Text[] experienceText = new Text[3];
    public Slider[] experienceSliders = new Slider[3];
    public Image[] characterImage = new Image[3];
    public GameObject[] characterStatHolder = new GameObject[3];

    private CharacterStats[] playerStats;

    void Start()
    {
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
    }

    public void CloseMenu()
    {
        for (int index = 0; index < windows.Length; index++)
        {
            windows[index].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }
}
