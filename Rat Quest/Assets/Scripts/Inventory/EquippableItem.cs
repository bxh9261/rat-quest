using System;
using UnityEngine;

//All of the equipment slots we can have in the game
public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves, 
    Boots,
    Weapon1,
   // Weapon2,
   // Accessory1,
   // Accessory2,
}

//STAT VALUES FOR EQUIPMENT
[CreateAssetMenu]
public class EquippableItem : Item
{
    //Stat values
    public int Rat;
    [Space]

    //STAT BONUSES
    public float RatBonus;
    [Space]

    //The equipment list
    public EquipmentType EquipmentType;

    internal void Unequip(InventoryManager inventoryManager)
    {
        //throw new NotImplementedException();
    }

    internal void Equip(InventoryManager inventoryManager)
    {
       // throw new NotImplementedException();
    }
}
