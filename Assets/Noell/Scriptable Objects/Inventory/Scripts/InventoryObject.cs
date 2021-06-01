using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable() {
        #if UNITY_EDITOR
        //Sets the database to where the file is in editor
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
        #else 
        database = Resources.Load<ItemDatabaseObject>("Database");
        #endif
    }
    public void AddItem(ItemObject _item, int _amount)
    {

        //Loop through the list of items to see if we have the item on the list already.
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                //Adds to the amount of the item using the method from the InventorySlot class.
                Container[i].AddAmount(_amount);
                //breaks out of the loop
                return;
            }
        }
        //Adds the new item to the list with the amount if it is not already there
        Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
    }

    //Our saving will be done by using a binary formatter in the json utility. First we use the json utility to serialize our scriptable object to a string then
    //We will use the binary formatter and the file stream to create a file and write the string into that file save it to a given location (our savepath)
    public void Save()
    {
        //This = the scriptable object, true = pretty print
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        //We're using concat to combine more strings into one. Uses less memory than doing + " "
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //Serialize the file
        bf.Serialize(file, saveData);
        file.Close();

    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //Converting the file back
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this /*This = The object that should be overwritten / The object that we want to paste it to = The scriptable object*/);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        //When Unity serializes the scriptableobject, look through all the items in the container and repopulate the items so that they match with the items ID
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
    }

}


[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;

    //Constructor
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}