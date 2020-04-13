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
    //FLAT STAT BONUSES
    public float STRBonus;
    public float DEFBonus;
    public float VITBonus;
    [Space]

    //PERCENT STAT BONUSES
    public float STRPercentBonus;
    public float DEFPercentBonus;
    public float VITPercentBonus;
    [Space]

    //The equipment list
    public EquipmentType EquipmentType;

    //The player unequipped something, so remove the stat value from the pool
    internal void Unequip(InventoryManager i)
    {
        i.Strength.RemoveAllModifiersFromSource(this);
        i.Defense.RemoveAllModifiersFromSource(this);
        i.Vitality.RemoveAllModifiersFromSource(this);
    }

    //If the player equips an item, update the stats
    internal void Equip(InventoryManager i)
    {
        //FLAT BONUSES
        if(STRBonus != 0) { i.Strength.AddModifiers(new StatModifier(STRBonus, StatModType.Flat, this)); }
        if(DEFBonus != 0) { i.Defense.AddModifiers(new StatModifier(DEFBonus, StatModType.Flat, this)); }
        if(VITBonus != 0) { i.Vitality.AddModifiers(new StatModifier(VITBonus, StatModType.Flat, this)); }

        //FOR ADDITIONAL PERCENT BONUSES
        if (STRPercentBonus != 0) { i.Strength.AddModifiers(new StatModifier(STRPercentBonus, StatModType.AddPercent, this)); }
        if (DEFPercentBonus != 0) { i.Defense.AddModifiers(new StatModifier(DEFPercentBonus, StatModType.AddPercent, this)); }
        if (VITPercentBonus != 0) { i.Vitality.AddModifiers(new StatModifier(VITPercentBonus, StatModType.AddPercent, this)); }

        /*
        //FOR MULTIPLIER PERCENT BONUSES
        if (STRPercentBonus != 0) { i.Strength.AddModifiers(new StatModifier(STRPercentBonus, StatModType.MultPercent, this)); }
        if (DEFPercentBonus != 0) { i.Defense.AddModifiers(new StatModifier(DEFPercentBonus, StatModType.MultPercent, this)); }
        if (VITPercentBonus != 0) { i.Vitality.AddModifiers(new StatModifier(VITPercentBonus, StatModType.MultPercent, this)); }
        */
    }
}
