public class EquipmentSlot : ItemSlot
{
    //Read in the equipment type value
    public EquipmentType EquipmentType;

    //Set the slot values
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if(item == null)
        {
            return true;
        }

        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
    }
}
