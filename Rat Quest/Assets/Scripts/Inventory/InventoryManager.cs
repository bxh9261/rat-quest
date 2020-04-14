using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //Variable for the main inventory
    [SerializeField]
    Inventory inventory;

    //Variable for the equipment panel
    [SerializeField]
    EquipmentPanel equipmentPanel;

    //Variable for the character stats panel
    [SerializeField]
    StatPanel statPanel;

    //Variable for when dragging an item in the inventory
    [SerializeField]
    Image draggableItem;

    //Variable for getting the tooltip
    [SerializeField]
    Tooltip tooltip;

    //Character stats
    public CharacterStat Strength;
    public CharacterStat Defense;
    public CharacterStat Vitality;

    private ItemSlot draggedSlot;

    private void Awake()
    {
        //Set the stat types and then update the stat values
        statPanel.SetStats(Strength, Defense, Vitality);
        statPanel.UpdateStatValues();

        //Setup Events
        //Right Click
        inventory.onRightClick += Equip;
        equipmentPanel.onRightClick += Unequip;
        //Drag Event
        inventory.onDrag += Drag;
        equipmentPanel.onDrag += Drag;
        //Begin Drag Event
        inventory.onBeginDrag += BeginDrag;
        equipmentPanel.onBeginDrag += BeginDrag;
        //End Drag Event
        inventory.onEndDrag += EndDrag;
        equipmentPanel.onEndDrag += EndDrag;
        //Drop Event
        inventory.onDrop += Drop;
        equipmentPanel.onDrop += Drop;
        //On Pointer Enter
        inventory.onPointerEnter += ShowTooltip;
        equipmentPanel.onPointerEnter += ShowTooltip;
        //On Pointer Exit
        inventory.onPointerExit += HideTooltip;
        equipmentPanel.onPointerExit += HideTooltip;

    }

    //When the function validates, set the tooltip
    private void OnValidate()
    {
        if(tooltip == null)
        {
            tooltip = FindObjectOfType<Tooltip>();
        }
    }

    //SHOWING THE TOOLTIP
    //ITEMS
    //Check for the item type, and send the appropriate information
    private void ShowTooltip(ItemSlot itemSlot)
    {
        tooltip.ShowTooltip(itemSlot);
    }

    //STATS

    //Stops showing the tooltip
    private void HideTooltip(ItemSlot itemSlot)
    {
        tooltip.HideTooltip();
    }

    //Right Click
    private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    //Begin Drag
    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    //While Dragging
    private void Drag(ItemSlot itemSlot)
    {
        if(draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    //End Drag
    private void EndDrag(ItemSlot itemSlot)
    { 
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    //On Drop
    private void Drop(ItemSlot dropItemSlot)
    {
        //Check if we can swap the two items using their slots
        if(dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {         
            EquippableItem dragItem = draggedSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(draggedSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }
            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }
            statPanel.UpdateStatValues();

            //Swap the two items being dragged
            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }


    public void Equip(EquippableItem item)
    {
        //If we remove the item from the inventory
        if(inventory.RemoveItem(item))
        {
            //Try to add it to the equipment panel
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                //If the slots full...
                if(previousItem != null)
                {
                    //Swap the items out
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
        }
        //If we can't equip the item...
        else
        {
            //Put it back into our inventory
            inventory.AddItem(item);
        }
    }

    //Called when removing equipment
    public void Unequip(EquippableItem item)
    {
        //If the inventory's not full, and we can remove the item
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
