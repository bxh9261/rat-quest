using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{
    private const float doubleClickTime = .2f;
    private float lastClickTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float startTime = Time.time - lastClickTime;

            if (lastClickTime <= doubleClickTime)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    Destroy(GameObject.Find(hit.collider.gameObject.name));
                }
            }

            lastClickTime = Time.time;
        }
    }
}
