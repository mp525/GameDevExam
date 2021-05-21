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
    public GameObject prefab;

    public ItemType type;

    //Creates a text area to write a description in the editor
    [TextArea (15,20)]
    public string description;

}
