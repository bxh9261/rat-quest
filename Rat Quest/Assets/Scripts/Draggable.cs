﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isPickedUp = false;

    private const float doubleClickTime = .2f;
    private float lastClickTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(isPickedUp)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }

    }

    void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isPickedUp = true;

            Debug.Log("mouse button down");
        }
    }

    void OnMouseUp()
    {
        isPickedUp = false;
    }
}
