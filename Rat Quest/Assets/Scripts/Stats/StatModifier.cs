//Code used from Kryzarel's character stats tutorial
//Link: https://www.youtube.com/watch?v=SH25f3cXBVc&list=PLm7W8dbdflojT-OqfBJvqK6L9LRwKmymz

//Check if the modifier is a percent based modifier, or a flat number modifier
public enum StatModType
{
    Flat = 100, 
    AddPercent = 200,
    MultPercent = 300,
}


public class StatModifier
{
    //Variable to hold the value
    public readonly float Value;

    //Readonly variable for what type of number the modifier is
    public readonly StatModType Type;

    //Variable for what order the calculations should be done in
    //We want the percent modifiers to be calculated last, so this is necessary
    public readonly int Order;

    //Variable to hold where the source of the stat modifier is from
    //EX: is it coming from your sword, your shield, or your armor?
    public readonly object Source;

    //Read-in and set the value and the modifier type, and what order they should be added
    public StatModifier(float value, StatModType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    //A constructor that calls when only two variables are used
    //It then goes and calls the constructor above and sets it with the correct variables
    //Flat values will be 0 and Percent values will be 1s, making Flat values get called first
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
