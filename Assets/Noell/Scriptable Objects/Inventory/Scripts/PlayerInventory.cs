using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    [SerializeField] KeyCode saveKey = KeyCode.Space;
    [SerializeField] KeyCode loadKey = KeyCode.Tab;

    public void OnTriggerEnter(Collider other) 
    {
        var item = other.GetComponent<GroundItem>();

        if(item)
        {
            //Adds item we collided with to the inventory
            inventory.AddItem(new Item(item.item), 1);

            //Destroys the item we collided with
            Destroy(other.gameObject);
        }

    }

    private void Update() 
    {
        if(Input.GetKeyDown(saveKey))    
        {
            inventory.Save();
            Debug.Log("Saved inventory!");
        }
        if(Input.GetKeyDown(loadKey))    
        {
            inventory.Load();
            Debug.Log("Loaded inventory!");

        }
    }

    private void OnApplicationQuit() 
    {
         inventory.Container.Items = new InventorySlot[15];    
    }
}
