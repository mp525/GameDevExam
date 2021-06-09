using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    //We use two dictionaries instead of one instead of using a double for loop 
    // public Dictionary<Item, int> GetId = new Dictionary<Item, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject >();

    public void OnAfterDeserialize()
    {
        //Clears out dictionary so we don't duplicate anything
        // GetId = new Dictionary<Item, int>();

        //Everytime Unity serializes the scriptable object, it will populate the dictionary
        //Loops through all the items
        for (int i = 0; i < Items.Length; i++)
        {
            //Adds the item's ID
            // GetId.Add(Items[i], i); 
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);            
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();

    }

}
