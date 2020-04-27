using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Points")]
public class PointEffect : ItemEffects
{
    public int pointAmount;

    public override void ExecuseEffect(ConsumableItem item, InventoryManager inventory)
    {
        inventory.score.AddScore(pointAmount);
    }

    public override string GetItemDescription()
    {
        return "Gives you " + pointAmount + " points!~ Hurray!";
    }
}
