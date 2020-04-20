using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Items/Usable Item")]
public class ConsumableItem : Item
{
    public bool isConsumable;

    public List<ItemEffects> Effects;

    public virtual void Use(InventoryManager inventory)
    {
        foreach (ItemEffects effect in Effects)
        {
            effect.ExecuseEffect(this, inventory);
        }
    }

    public override string GetItemType()
    {
        return isConsumable ? "Consumable" : "Basic Item";
    }

    public override string GetItemDescription()
    {
        //Reset the string length
        sb.Length = 0;

        foreach(ItemEffects effect in Effects)
        {
            sb.Append(effect.GetItemDescription() + " " + Description);
        }

        return sb.ToString();
    }
}
