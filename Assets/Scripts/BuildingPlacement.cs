using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private PlaceableBuilding placeableBuilding;
    private Transform currentBuilding;

    private bool hasPlaced;

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
             if (Physics.Raycast(ray, out hit, 1000f, 1<<7)) {
                 if(hit.collider.gameObject.tag.Equals("Terrain")){
                     currentBuilding.position = hit.point;
                 }
                

            /* Vector3 m = Input.mousePosition;
            m = new Vector3(m.x,m.y,transform.position.y);
            Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);
            currentBuilding.position = new Vector3(p.x,10.0,p.z);  */ //10.0 for at nå indre ø højde
        }
        if(Input.GetMouseButtonDown(0)){
            if(IsLegalPosition())
                hasPlaced = true;
        }
    }
    }

    bool IsLegalPosition(){
        if(placeableBuilding.colliders.Count > 0){
            return false;
        }
        return true;
    }

    public void SetItem(GameObject b){
        hasPlaced = false;
        currentBuilding = ((GameObject)Instantiate(b)).transform;
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
    }
}
