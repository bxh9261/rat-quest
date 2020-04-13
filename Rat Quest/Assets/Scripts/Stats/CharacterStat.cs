using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//Code used from Kryzarel's character stats tutorial
//Link: https://www.youtube.com/watch?v=SH25f3cXBVc&list=PLm7W8dbdflojT-OqfBJvqK6L9LRwKmymz

[Serializable]
public class CharacterStat
{
    //Variable to read in the base stat value
    public float BaseValue;

    //Bool check to see if the value needs to be recalculated
    protected bool needReCalc = true;
    //Variable to hold the most recent calculation
    protected float _val;
    //Variable to store the last base value
    protected float lastBaseValue = float.MinValue;

    //Quick get-value for the total stat (BaseStat + modifiers)
    public virtual float Value
    {
        get
        {
            //If we need to recalulate the value...
            //Or if the base values don't match
            if(needReCalc || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _val = CalculateFinalValue();
                needReCalc = false;
            }

            //Return the last calculated value
            return _val;
        }
    }

    //Variable for the stat modifier values
    //The reason why it is a list is because we can have multiple sources of modifiers
    //EX: we have a sword, a shield, and armor that all increase DEFENSE
    protected readonly List<StatModifier> statModifiers;

    //Used for safely exposing the statModifier list
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    //Base constructor
    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    //Read in and set the base stat value
    //Then read in it's modifiers
    public CharacterStat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    //Add the stat modifiers to the list
    public virtual void AddModifiers(StatModifier mod)
    {
        //A new value was added, so we need to recalculate.
        needReCalc = true;
        statModifiers.Add(mod);
        //Sort the stat modifiers' addition order
        statModifiers.Sort(CompareModifierOrder);
    }

    //Remove the particular stat from the list
    public virtual bool RemoveModifier(StatModifier mod)
    {
        //A value was removed, so we need to recalculate.
        if (statModifiers.Remove(mod))
        {
            needReCalc = true;
            return true;
        }
        //Nothing was removed. return false
        return false;
    }

    //Remove all of the modifiers based off of the source item that had them
    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        //Varaible to check if we removed the modifier and item
        bool removed = false;

        //Loop through the item list
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            //If we found the source of the modifier
            if(statModifiers[i].Source == source)
            {
                //We'll have to recalculate the modifier total
                needReCalc = true;
                removed = true;
                //Remove the modifier from the list
                statModifiers.RemoveAt(i);
            }
        }

        //Return if the item was removed or not
        return removed;
    }

    //Compare the order in which the modifiers should be added
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        //Just use a simple sort to do so
        if (a.Order < b.Order)
        {
            return -1;
        }
        else if (a.Order > b.Order)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    //Go through the modifier list and add them all together
    protected virtual float CalculateFinalValue()
    {
        //Var for the modifier total
        float finalValue = BaseValue;

        //Variable for getting the total percentage of the modifiers
        float sumPercentAddtion = 0;

        for(int i = 0; i < statModifiers.Count; i++)
        {
            //Create a base for the modifier
            StatModifier mod = statModifiers[i];

            //The value is just a plain number
            if(mod.Type == StatModType.Flat)
            {
                //Loop through and get the total
                finalValue += mod.Value;
            }
            //The value is a flat percent
            else if (mod.Type == StatModType.AddPercent)
            {
                //Add to the total flat percents
                sumPercentAddtion += mod.Value;

                //Keep adding until we get a different percent type, or unitl we reach the end of the list
                if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.AddPercent)
                {
                    //Add it to the total
                    finalValue *= 1 + sumPercentAddtion;
                    //Clear the value
                    sumPercentAddtion = 0;
                }
            }
            //The value is a percent scalar
            else if (mod.Type == StatModType.MultPercent)
            {
                finalValue *= 1 + mod.Value;
            }
        }

        //Round the value to keep from errors
        return (float) Math.Round(finalValue, 4);
    }
}
