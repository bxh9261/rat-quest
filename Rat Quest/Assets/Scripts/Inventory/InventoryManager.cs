using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;

    [SerializeField]
    EquipmentPanel equipmentPanel;

    [SerializeField]
    Image draggableItem;

    private ItemSlot draggedSlot;

    private void Awake()
    {

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
            /*
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
            */

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
                }
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
            inventory.AddItem(item);
        }
    }
}
