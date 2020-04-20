using System;
using UnityEngine;

//All of the equipment slots we can have in the game
public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves, 
    Boots,
    //Weapon1,
   // Weapon2,
   // Accessory1,
   // Accessory2,
   Weapon,
   Shield
}

//STAT VALUES FOR EQUIPMENT
[CreateAssetMenu(menuName = "Items/Equippable Item")]
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

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

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

    public override string GetItemDescription()
    {
        //Reset the string length
        sb.Length = 0;

        //Print the stats

        //Strength
        AddStats(STRBonus, "Strength");
        AddStats(STRPercentBonus, "Strength", isPercent: true);

        //Defense
        AddStats(DEFBonus, "Defense");
        AddStats(DEFPercentBonus, "Defense", isPercent: true);

        //Vitality
        AddStats(VITBonus, "Vitality");
        AddStats(VITPercentBonus, "Vitality", isPercent: true);

        //Go onto the next!
        sb.AppendLine();
        sb.Append(Description);

        return sb.ToString();

    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    //Used for printing the stats for the tooltip
    private void AddStats(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            //If this isn't the first line...
            if (sb.Length > 0)
            {
                //Go onto the next!
                sb.AppendLine();
            }

            //If the value's positive
            if (value > 0)
            {
                //Add the plus symbol to it!
                //We don't have to worry about negative numbers
                //Because the - will already be there.
                sb.Append("+");
            }

            //Print for the percent value
            if (isPercent)
            {
                //Print the value
                sb.Append(value * 100);
                //Then give a space
                sb.Append("% ");
            }
            //Print for the normal flat value
            else
            {
                //Print the value
                sb.Append(value);
                //Then give a space
                sb.Append(" ");
            }

            //Then the name
            sb.Append(statName);
        }
    }
}
