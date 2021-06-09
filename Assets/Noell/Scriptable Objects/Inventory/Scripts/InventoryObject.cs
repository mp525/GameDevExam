using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;

    // private void OnEnable() {
    //     #if UNITY_EDITOR
    //     //Sets the database to where the file is in editor
    //     database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
    //     #else 
    //     database = Resources.Load<ItemDatabaseObject>("Database");
    //     #endif
    // }
    public void AddItem(Item _item, int _amount)
    {

        //Loop through the list of items to see if we have the item on the list already.
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
            {
                //Adds to the amount of the item using the method from the InventorySlot class.
                Container.Items[i].AddAmount(_amount);
                //breaks out of the loop
                return;
            }
        }
        //Adds the new item to the list with the amount if it is not already there
        Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
    }

    //Our saving will be done by using a binary formatter in the json utility. First we use the json utility to serialize our scriptable object to a string then
    //We will use the binary formatter and the file stream to create a file and write the string into that file save it to a given location (our savepath)
    //Changed it to use IFormatter.
    [ContextMenu("Save")]
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

        // IFormatter formatter = new BinaryFormatter();
        // Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        // formatter.Serialize(stream, Container);
        // stream.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //Converting the file back
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this /*This = The object that should be overwritten / The object that we want to paste it to = The scriptable object*/);
            file.Close();
            // IFormatter formatter = new BinaryFormatter();
            // Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath),FileMode.Open,FileAccess.Read);
            // Container = (Inventory)formatter.Deserialize(stream);
            // stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }

    // public void OnAfterDeserialize()
    // {
    //     //When Unity serializes the scriptableobject, look through all the items in the container and repopulate the items so that they match with the items ID
    //     for (int i = 0; i < Container.Items.Count; i++)
    //     {
    //         Container.Items[i].item = database.GetItem[Container.Items[i].ID];
    //     }
    // }

    // public void OnBeforeSerialize()
    // {
    // }

}


[System.Serializable]
public class Inventory 
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;

    //Constructor
    public InventorySlot(int _id, Item _item, int _amount)
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