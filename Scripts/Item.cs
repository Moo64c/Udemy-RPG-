using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Description")]
    public string itemName;
    public string description;
    public int value;

    public Sprite itemSprite;
    
    [Header("Item Details")]
    // Generic "amount" value;
    public int amountToChange;
    public bool affectHP, affectMP, affectStrength;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    public void Use(int characterToUseOn)
    {
        CharacterStats selectedCharacter = GameManager.instance.playerStats[characterToUseOn];

        if (isItem)
        {
            if (affectHP)
            {
                selectedCharacter.currentHealthPoints += amountToChange;
                if (selectedCharacter.currentHealthPoints > selectedCharacter.maxHealthPoints)
                {
                    selectedCharacter.currentHealthPoints = selectedCharacter.maxHealthPoints;
                }
            }

            if (affectMP)
            {
                selectedCharacter.currentMagicPoints += amountToChange;
                if (selectedCharacter.currentMagicPoints > selectedCharacter.maxMagicPoints)
                {
                    selectedCharacter.currentMagicPoints = selectedCharacter.maxMagicPoints;
                }
            }

            if (affectStrength)
            {
                selectedCharacter.strength += amountToChange;
            }

        }
        
        if (isWeapon)
        {
            if (selectedCharacter.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedWeapon);
            }
            selectedCharacter.equippedWeapon = itemName;
            selectedCharacter.weaponPower = weaponStrength;
        }
        
        if (isArmor) {
            Debug.Log("is armor!");
            if (selectedCharacter.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedArmor);
            }
            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
