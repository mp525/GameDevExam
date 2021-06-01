using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    //We use two dictionaries instead of one instead of using a double for loop 
    public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject >();

    public void OnAfterDeserialize()
    {
        //Clears out dictionary so we don't duplicate anything
        GetId = new Dictionary<ItemObject, int>();
        GetItem = new Dictionary<int, ItemObject>();

        //Everytime Unity serializes the scriptable object, it will populate the dictionary
        //Loops through all the items
        for (int i = 0; i < Items.Length; i++)
        {
            //Adds the item's ID
            GetId.Add(Items[i], i);            
            GetItem.Add(i, Items[i]);            
        }
    }

    public void OnBeforeSerialize()
    {
    }

}
