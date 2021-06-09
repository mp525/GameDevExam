using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The types of items we can create 
//To create a new item right click in the editor
public enum ItemType
{
    Food,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;

    public Sprite uiDisplay;

    public ItemType type;

    //Creates a text area to write a description in the editor
    [TextArea (15,20)]
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}
