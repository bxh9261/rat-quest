using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DeleteItem : MonoBehaviour, IDropHandler
{
    public event Action OnDropEvent;

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent();
    }
}
