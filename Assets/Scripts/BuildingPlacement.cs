using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacement : MonoBehaviour
{
    private PlaceableBuilding placeableBuilding;
    private Transform currentBuilding;

    public GameObject playerHand;
    public bool hasPlaced;

    private float rotY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBuilding != null && !hasPlaced){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
             if (Physics.Raycast(ray, out hit, 10.0f, 1<<7)) {
                 if(hit.collider.gameObject.tag.Equals("Terrain")){
                     currentBuilding.position = hit.point;
                 }
                
            //Wont need this probably
            /* Vector3 m = Input.mousePosition;
            m = new Vector3(m.x,m.y,transform.position.y);
            Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);
            currentBuilding.position = new Vector3(p.x,10.0,p.z);  */ //10.0 for at nå indre ø højde
        }

        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Click");
            if(IsLegalPosition()){
                hasPlaced = true;
                
            }
                
        }

        if(Input.GetMouseButtonDown(1)){
            Debug.Log("Right click!");
            CancelCurrentBuild();
        }

        if(Input.mouseScrollDelta.y > 0.0f){
            rotY += 2.0f;
            currentBuilding.rotation = Quaternion.Euler(0, rotY, 0);
        }
        if(Input.mouseScrollDelta.y < 0.0f){
            rotY -= 2.0f;
            currentBuilding.rotation = Quaternion.Euler(0, rotY, 0);
        }
    }
    
    }

    internal void CancelCurrentBuild()
    {
        Destroy(currentBuilding.gameObject);
    }

    bool HasResources(){
        //TODO: Check building requirements and compare to inventory resources (campfire:10stone and 5 wood?)
        /* 
        if(placeableBuilding.requirements is in inventory){
            return true;
        }
        else{
            return false;
        }
         */
        return true;
    }

    bool IsLegalPosition(){
        if(placeableBuilding.colliders.Count > 0){
            return false;
        }
         /* if(IsPointerOverUIObject()){
             Debug.Log("Is over UI");
            return false;
        }  */
        
        return true;
    }

    public void SetItem(GameObject b){
        if(b.name=="torch"){
            hasPlaced = false;

             GameObject childObject = Instantiate(b) as GameObject;
             //Position
             childObject.transform.position=new Vector3(playerHand.transform.position.x,playerHand.transform.position.y,playerHand.transform.position.z);
             //Rotation
              Quaternion rotation = Quaternion.Euler(50, playerHand.transform.rotation.y, playerHand.transform.rotation.z);
             childObject.transform.rotation=rotation;
             
            childObject.transform.parent = playerHand.transform;
           
        }else{
             hasPlaced = false;
        currentBuilding = ((GameObject)Instantiate(b)).transform;
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
        }
       
    }

     public static bool IsPointerOverUIObject()
    {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
    }

}
