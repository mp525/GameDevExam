using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;

    public InventoryObject inventory;

    public int X_START;

    public int Y_START;

    public int X_SPACE_BETWEEN_ITEM;

    public int NUMBER_OF_COLUMNS;

    public int Y_SPACE_BETWEEN_ITEM;

    public PlayerManager playerManager;


    // Changing  things to de couple Inventory from Display Inventory.

    // public Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();


    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
        if(Input.GetMouseButtonDown(1)){
                Debug.Log("Consume");
                inventory.ConsumeItem(mouseItem.hoverSlot.item, playerManager);
            }
    }


    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            //The prefab we wanna spawn, vector3.zero = the inital position of 0, Quaternion.identify gives it a rotation of 0
            // and transform says to set the parent of the object we're spawning to the transform of the object our DisplayInventory script is attached to.
            // var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);

            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate{OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate{OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate{OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate{OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate{OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            //0 or more means it has an item in it
            if (_slot.Value.ID >= 0)
            {
                //FIND NOTE
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                //1 here = 255. So this will be pure white 100% alpha   
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                //If the amount is 1 put nothing because the item sprite being there shows that there is one. Else put the amount -> ToString with commas.
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");

            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                //Change the alpha to 0 so we can't see the square.
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                //There will be no text displayed.
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void AddEvent(GameObject gameObject, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        //We grab the event trigger from our gameobject
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        //Setting the type from the passed in type
        eventTrigger.eventID = type;
        //Listen for the action
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    //For Events

    public void OnEnter(GameObject obj)
    {
        //Sets our hoverObject to the slot that we are hovering over
        mouseItem.hoverObject = obj;
        Debug.Log("Hover on " + mouseItem.hoverObject.name);
        
        //Checks if the object we're hovering over is an itemsDisplayed object
        if(itemsDisplayed.ContainsKey(obj))
        {
            
            //If it is we will set the mouse's hoverSlot to that inventory slot (represented as hoverSlot)
            mouseItem.hoverSlot = itemsDisplayed[obj];
            
        }
        
    }
    public void OnExit(GameObject obj)
    {
        //Sets everything to null again on exit.
        mouseItem.hoverObject = null;       
        mouseItem.hoverSlot = null;
    }
    public void OnDragStart(GameObject obj)
    {
        //Create visual representation of the object we want to drag. Has nothing to do with the system part of the item.
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        //Same size as the item we're clicking on
        rt.sizeDelta = new Vector2(50,50);
        mouseObject.transform.SetParent(transform.parent);
        if(itemsDisplayed[obj].ID >= 0)
        {
            //There's an item on the inventory slot
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            //So that it won't get the way of our mouse and will never fire the pointer on enter
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        if(mouseItem.hoverObject)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObject]);
        }
        else
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        //When we stop dragging something -> Delete it (Otherwise there will just be a bunch of sprites left on the screen)
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if(mouseItem.obj != null)
        {
            //Sets the item's position to the mouse's position
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
            
        }

    }

    //Gets position depending on the slots available.
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    //To always have a reference of the object 
    public class MouseItem
    {
        public GameObject obj;
        public InventorySlot item;
        public InventorySlot hoverSlot;
        public GameObject hoverObject;
    }
}
