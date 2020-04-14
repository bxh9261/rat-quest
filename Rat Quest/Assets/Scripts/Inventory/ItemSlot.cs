using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image Image;

    public event Action<ItemSlot> onRightClick;
    public event Action<ItemSlot> onBeginDrag;
    public event Action<ItemSlot> onEndDrag;
    public event Action<ItemSlot> onDrag;
    public event Action<ItemSlot> onDrop;
    public event Action<ItemSlot> onPointerEnter;
    public event Action<ItemSlot> onPointerExit;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1,1,1, 0);

    //Read-in value for the item
    public Item _item;
    //Actual item
    public Item Item
    {
        //Read in the value
        get { return _item; }
        set
        {
            //Set the item
            _item = value;

            //If there isn't a item...
            if(_item == null)
            {
                //Don't draw an image
                Image.color = disabledColor;
            }
            else
            {
                //There is an item, so draw the image in the slot
                Image.sprite = _item.Icon;
                Image.color = normalColor;
            }
        }
    }

    //Used for setting the image-slot's start images
    protected virtual void OnValidate()
    {
        //If the image slots image isn't set yet...
        if (Image == null)
        {
            //Set the image to itself
            Image = GetComponent<Image>();
        }
    }

    //This always returns true because the inventory is boundless
    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(onRightClick != null)
            {
                onRightClick(this);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
        {
            onEndDrag(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null)
        {
            onDrop(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(onPointerEnter != null)
        {
            onPointerEnter(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExit != null)
        {
            onPointerExit(this);
        }
    }
}
