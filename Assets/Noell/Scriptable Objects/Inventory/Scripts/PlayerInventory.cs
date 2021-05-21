using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other) 
    {
        var item = other.GetComponent<Item>();

        if(item)
        {
            //Adds item we collided with to the inventory
            inventory.AddItem(item.item, 1);

            //Destroys the item we collided with
            Destroy(other.gameObject);
        }

    }

    private void OnApplicationQuit() 
    {
        inventory.Container.Clear();    
    }
}
