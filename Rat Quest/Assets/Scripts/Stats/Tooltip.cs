using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Tooltip : MonoBehaviour
{
    //Read in for the tooltip's text variables
    [SerializeField] Text NameText;
    [SerializeField] Text InfoText;
    [SerializeField] Text StatsText;

    //Used for building the item's strings
    private StringBuilder sb = new StringBuilder();

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;

        float width = rectTransform.sizeDelta.x;
        float height = rectTransform.sizeDelta.y;

        position.x -= width / 4;
        position.y += height / 4;

        this.transform.position = position;
    }

    //ITEMS
    //Show the tooltip for a equippable item
    public void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem item = itemSlot.Item as EquippableItem;
        if (item != null)
        {
            //Set the title to the item's name
            NameText.text = item.ItemName;

            //Set the info to what type the item is
            InfoText.text = item.EquipmentType.ToString();

            //Reset the string length
            sb.Length = 0;

            //Print the stats

            //Strength
            AddStats(item.STRBonus, "Strength");
            AddStats(item.STRPercentBonus, "Strength", isPercent: true);

            //Defense
            AddStats(item.DEFBonus, "Defense");
            AddStats(item.DEFPercentBonus, "Defense", isPercent: true);

            //Vitality
            AddStats(item.VITBonus, "Vitality");
            AddStats(item.VITPercentBonus, "Vitality", isPercent: true);

            //Go onto the next!
            sb.AppendLine();
            sb.Append(item.Description);

            //Set the text to the sb
            StatsText.text = sb.ToString();

            //Set the tooltip box to being active
            gameObject.SetActive(true);
        }
        else
        {
            Item standardItem = itemSlot.Item as Item;
            if(standardItem != null)
            {
                //Set the title to the item's name
                NameText.text = standardItem.ItemName;

                //Set the info to what type the item is
                InfoText.text = "Basic Item";

                StatsText.text = standardItem.Description;

                //Set the tooltip box to being active
                gameObject.SetActive(true);
            }
        }
    }

    //STATS
    //Show the tooltip for a equippable item
    public void ShowTooltip(CharacterStat stat, string statName)
    {
        NameText.text = statName;

        StatsText.text = GetStatTopText(stat, statName);

        InfoText.text = GetStatModifierText(stat);

        //Set the tooltip box to being active
        gameObject.SetActive(true);
    }

    //Hide the display
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    //Used for printing the stats for the tooltip
    private void AddStats(float value, string statName, bool isPercent = false)
    {
        if(value != 0)
        {
            //If this isn't the first line...
            if (sb.Length > 0)
            {
                //Go onto the next!
                sb.AppendLine();
            }

            //If the value's positive
            if(value > 0)
            {
                //Add the plus symbol to it!
                //We don't have to worry about negative numbers
                //Because the - will already be there.
                sb.Append("+");
            }

            //Print for the percent value
            if(isPercent)
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

    //Used for getting the stat modifier info for each stat
    private string GetStatTopText(CharacterStat stat, string statName)
    {
        //Reset the string builder
        sb.Length = 0;
        //Add the stats name
        sb.Append(statName);
        sb.Append(" ");
        //Add it's current value
        sb.Append(stat.Value);
      
        if(stat.Value != stat.BaseValue)
        {
            sb.Append(" (");
            //Print it's base value
            sb.Append(stat.BaseValue);

            //Check if the stat is adding or losing
            if (stat.Value > stat.BaseValue)
            {
                sb.Append("+");
            }

            //Show the new stat value
            sb.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
            sb.Append(")");
        }

        return sb.ToString();
    }

    private string GetStatModifierText(CharacterStat stat)
    {
        //Reset the string builder
        sb.Length = 0;
       
        foreach(StatModifier mod in stat.StatModifiers)
        {
            //If this isn't the first line...
            if (sb.Length > 0)
            {
                //Go onto the next!
                sb.AppendLine();
            }
            //If the value's positive
            if (stat.Value > 0)
            {
                //Add the plus symbol to it!
                //We don't have to worry about negative numbers
                //Because the - will already be there.
                sb.Append("+");
            }

            if(mod.Type == StatModType.Flat)
            {
                sb.Append(mod.Value);
            }
            else
            {
                sb.Append(mod.Value * 100);
                sb.Append("%");
            }

            EquippableItem item = mod.Source as EquippableItem;

            if(item != null)
            {
                sb.Append(" ");
                sb.Append(item.ItemName);
            }
            else
            {
                Debug.LogError("Modifier is not an equippable item! Uh oh!");
            }
        }

        //Return the string builder
        return sb.ToString();
    }
}
