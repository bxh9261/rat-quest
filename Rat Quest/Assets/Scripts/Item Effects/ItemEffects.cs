using UnityEngine;

public abstract class ItemEffects : ScriptableObject
{
    public abstract void ExecuseEffect(ConsumableItem item, InventoryManager inventory);
    public abstract string GetItemDescription();
}

