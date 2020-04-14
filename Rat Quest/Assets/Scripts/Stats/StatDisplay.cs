using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Code used from Kryzarel's Inventory Tutorial
//Link: https://www.youtube.com/watch?v=ez-YTf64Jn4&list=PLm7W8dbdfloj4CWX8RS5_cnDWVn1Q6u9Q&index=3

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat
    {
        get { return _stat;  }
        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameText.text = _name;
        }
    }

    [SerializeField]
    public Text nameText;

    [SerializeField]
    public Text valueText;

    [SerializeField]
    Tooltip tooltip;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];


        //When the function validates, set the tooltip
        if (tooltip == null)
        {
            tooltip = FindObjectOfType<Tooltip>();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }
}
