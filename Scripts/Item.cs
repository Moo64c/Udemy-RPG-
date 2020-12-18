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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
