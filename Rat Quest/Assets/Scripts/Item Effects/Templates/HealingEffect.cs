using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Healing")]
public class HealingEffect : ItemEffects
{
    public float healingAmount;

    public override void ExecuseEffect(ConsumableItem item, InventoryManager inventory)
    {
        inventory.sm.restoreHealth(healingAmount);
    }

    public override string GetItemDescription()
    {
        return "Heals for " + healingAmount + " hp.";
    }
}
