using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public string characterName;

    public int playerLevel = 1;
    public int currentExperience = 0;
    public int maxLevel = 100;
    public int baseExperience = 1000;

    public int currentHealthPoints;
    public int maxHealthPoints = 100;

    public int currentMagicPoints;
    public int maxMagicPoints = 30;

    public int strength;
    public int defense;
    public int weaponPower;
    public int armorPower;

    public string equippedWeapon;
    public string equippedArmor;

    public Sprite characterImage;
    public int[] experienceToNextLevel;

    private int[] magicPointsLevelBonus;

    void Start()
    {
        experienceToNextLevel = new int[maxLevel];
        magicPointsLevelBonus = new int[maxLevel];
        experienceToNextLevel[0] = baseExperience;
        for(int index = 1; index < experienceToNextLevel.Length; index++)
        {
            experienceToNextLevel[index] = Mathf.FloorToInt(experienceToNextLevel[index - 1] * 1.05f);
            magicPointsLevelBonus[index] = ((index % 3) % 2) * (index + 5);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExperience(1000);
        }
    }

    public void AddExperience(int experienceToAdd)
    {
        if (playerLevel >= maxLevel)
        {
            return;
        }

        currentExperience += experienceToAdd;
        if (currentExperience >= experienceToNextLevel[playerLevel - 1])
        {
            currentExperience -= experienceToNextLevel[playerLevel - 1];
            playerLevel++;

            if (playerLevel >= maxLevel)
            {
                currentExperience = 0;
            }

            // Determine which stat gets increased: Strength or Defense.
            defense  +=      playerLevel % 2;
            strength += 1 - (playerLevel % 2);

            maxHealthPoints = Mathf.FloorToInt(maxHealthPoints * 1.05f);
            currentHealthPoints = maxHealthPoints;

            maxMagicPoints += magicPointsLevelBonus[playerLevel];
            currentMagicPoints = maxMagicPoints;
        }
    }
}
