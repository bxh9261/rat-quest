using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Item> startingItems;

    [SerializeField]
    Transform itemsParent;

    [SerializeField]
    ItemSlot[] itemSlots;

    public event Action<ItemSlot> onRightClick;
    public event Action<ItemSlot> onBeginDrag;
    public event Action<ItemSlot> onEndDrag;
    public event Action<ItemSlot> onDrag;
    public event Action<ItemSlot> onDrop;
    public event Action<ItemSlot> onPointerEnter;
    public event Action<ItemSlot> onPointerExit;


    private void Start()
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].onRightClick += onRightClick;
            itemSlots[i].onDrag += onDrag;
            itemSlots[i].onBeginDrag += onBeginDrag;
            itemSlots[i].onEndDrag += onEndDrag;
            itemSlots[i].onDrop += onDrop;
            itemSlots[i].onPointerEnter += onPointerEnter;
            itemSlots[i].onPointerExit += onPointerExit;
        }

        SetStartingItems();
    }

    //On startup...
    private void OnValidate()
    {
        //If there's an inventory to draw...
        if (itemsParent != null)
        {
            //Go get it
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        //Refresh it's UI
        //SetStartingItems();
    }

    //Refreshses the UI to draw the contents of the inventory
    private void SetStartingItems()
    {
        int i = 0;

        //Loop through the inventory and draw the amount of items that exist
        //Draws from left to right slots
        for(; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i].GetCopy();
        }

        //If there isn't any items...
        for(; i < itemSlots.Length; i++)
        {
            //Set the slot images to null and don't draw them
            itemSlots[i].Item = null;
        }
    }

    //Logic to add an item to the inventory
    public bool AddItem(Item item)
    {
        //Loop through the item slots
        for(int i = 0; i < itemSlots.Length; i++)
        {
            //If the slot is empty...
            if(itemSlots[i].Item == null)
            {
                //Add the item to it
                itemSlots[i].Item = item;
                return true;
            }
        }
        //There were no empty slots
        return false;
    }

    //Remove an item from the inventory
    public bool RemoveItem(Item item)
    {
        //Loop through the item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //If the slot is full...
            if (itemSlots[i].Item == item)
            {
                //Remove the item from it
                itemSlots[i].Item = null;
                return true;
            }
        }
        //The slot was empty
        return false;
    }

    //Check to see if the inventory is full
    public bool IsFull()
    {
        //Loop through the item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //If the slot is empty...
            if (itemSlots[i].Item == null)
            {
                //return false because the inventory is not full
                return false;
            }
        }
        //The inventory is full
        return true;
    }
}
