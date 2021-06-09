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
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                //Adds to the amount of the item using the method from the InventorySlot class.
                Container.Items[i].AddAmount(_amount);
                //breaks out of the loop
                return;
            }
        }
        //Adds the new item to the list with the amount if it is not already there
        SetFirstEmptySlot(_item,_amount);
        // Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
    }

    public InventorySlot SetFirstEmptySlot(Item _item, int _amount)
    {
        //Check to find the first slot that's empty (e.g. the default id of -1)
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        //TO DO - Setup functionality for FULL inventory
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        //Make a temporary slot that has the values of item2
        InventorySlot temp = new InventorySlot(item2.ID, item2.item,item2.amount);
        //Update item2 to have the values of item 1
        item2.UpdateSlot(item1.ID,item1.item,item1.amount);
        //Update item1 to have the values of the temp item. So 1 and 2 switch places.
        item1.UpdateSlot(temp.ID, temp.item,temp.amount);

    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            //If the item in our inventory is = to the item that we're trying to remove
            if(Container.Items[i].item == _item)
            {
                //Sets it back to being empty/null
                Container.Items[i].UpdateSlot(-1, null , 0);
            }
        }
    }

    //Our saving will be done by using a binary formatter in the json utility. First we use the json utility to serialize our scriptable object to a string then
    //We will use the binary formatter and the file stream to create a file and write the string into that file save it to a given location (our savepath)
    //Changed it to use IFormatter.
    [ContextMenu("Save")]
    public void Save()
    {
        //This = the scriptable object, true = pretty print
        // string saveData = JsonUtility.ToJson(this, true);
        // BinaryFormatter bf = new BinaryFormatter();
        // //We're using concat to combine more strings into one. Uses less memory than doing + " "
        // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        // //Serialize the file
        // bf.Serialize(file, saveData);
        // file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            // BinaryFormatter bf = new BinaryFormatter();
            // FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            // //Converting the file back
            // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this /*This = The object that should be overwritten / The object that we want to paste it to = The scriptable object*/);
            // file.Close();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath),FileMode.Open,FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);

            //Swap out the stuff from the old container into the new.
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
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
    public InventorySlot[] Items = new InventorySlot[24];
    // public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item item;
    public int amount;

    //Constructor
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    //Constructor
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void UpdateSlot(int _id, Item _item, int _amount)
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