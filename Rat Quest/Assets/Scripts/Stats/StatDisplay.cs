using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code used from Kryzarel's Inventory Tutorial
//Link: https://www.youtube.com/watch?v=ez-YTf64Jn4&list=PLm7W8dbdfloj4CWX8RS5_cnDWVn1Q6u9Q&index=3

public class StatDisplay : MonoBehaviour
{
    public Text NameText;
    public Text ValueText;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameText = texts[0];
        ValueText = texts[1];
    }
}
