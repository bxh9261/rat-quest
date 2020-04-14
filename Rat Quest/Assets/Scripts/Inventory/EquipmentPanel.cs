using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<ItemSlot> onRightClick;
    public event Action<ItemSlot> onBeginDrag;
    public event Action<ItemSlot> onEndDrag;
    public event Action<ItemSlot> onDrag;
    public event Action<ItemSlot> onDrop;
    public event Action<ItemSlot> onPointerEnter;
    public event Action<ItemSlot> onPointerExit;


    private void Start()
    {
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].onRightClick += onRightClick;
            equipmentSlots[i].onDrag += onDrag;
            equipmentSlots[i].onBeginDrag += onBeginDrag;
            equipmentSlots[i].onEndDrag += onEndDrag;
            equipmentSlots[i].onDrop += onDrop;
            equipmentSlots[i].onPointerEnter += onPointerEnter;
            equipmentSlots[i].onPointerExit += onPointerExit;
        }
    }

    //Set the equipment slots on startup
    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    //Check to see if we can equip the item
    //The out parameter is the item we were currently wearing
    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        //Loop through all the slots
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            //If the item is the correct equipment type...
            if (equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                //They can equip it!
                //Set the last item to what was in the slot,
                //equip the new item, and return true
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                return true;
            }
        }

        //They can't, so return false
        previousItem = null;
        return false;
    }


    //Check to see if we can remove the itme
    //it's the same as AddItem, but now we're just looking for the item
    public bool RemoveItem(EquippableItem item)
    {
        //Loop through all the slots
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            //If the item is the correct equipment type...
            if (equipmentSlots[i].Item == item)
            {
                //They can remove it
                //Set the item to null and return true
                equipmentSlots[i].Item = null;
                return true;
            }
        }

        //They can't, so return false
        return false;
    }
}
