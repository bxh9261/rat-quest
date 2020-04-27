using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Damaging")]
public class HarmingEffect : ItemEffects
{
    public float harmingAmount;

    public override void ExecuseEffect(ConsumableItem item, InventoryManager inventory)
    {
        inventory.sm.DealDamageToEnemy(harmingAmount);
    }

    public override string GetItemDescription()
    {
        return "Does " + harmingAmount + " to the enemy.";
    }
}
