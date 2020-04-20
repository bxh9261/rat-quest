using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Items/Generic Item")]
public class Item : ScriptableObject
{
    //Read in the item's name
    public string ItemName;
    //Get it's image icon
    public Sprite Icon;
    //Used for the item's descriptions
    public string Description;

    //Used for building the item's strings
    protected static readonly StringBuilder sb = new StringBuilder();

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {
        
    }

    public virtual string GetItemType()
    {
        return "Basic Item";
    }

    public virtual string GetItemDescription()
    {
        return Description;
    }
}
