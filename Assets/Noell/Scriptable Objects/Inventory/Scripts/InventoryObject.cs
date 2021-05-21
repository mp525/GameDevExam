using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
   public List<InventorySlot> Container = new List<InventorySlot>();
   public void AddItem(ItemObject _item, int _amount)
   {
       bool hasItem = false;

       //Loop through the list of items to see if we have the item on the list already.
       for(int i = 0; i < Container.Count; i++)
       {
           if(Container[i].item == _item)
           {
               //Adds to the amount of the item using the method from the InventorySlot class.
               Container[i].AddAmount(_amount);
               //Sets boolean to true and breaks out of the loop
               hasItem = true;
               break;
           }
       }
       //If the list does not have the item
       if(!hasItem)
       {
           //Adds the new item to the list with the amount
           Container.Add(new InventorySlot(_item, _amount));
       }
    
   }
}


[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    //Constuctor
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}