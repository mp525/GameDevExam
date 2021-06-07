using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildings;

    private BuildingPlacement buildingPlacement;

    private bool menu = true;
    private bool builds = false;

        void Start()
    {
        buildingPlacement = GetComponent<BuildingPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        if(menu){
            BuildMenu();
        }
        if(builds){
            Buildings();
        }
    }

    void Buildings(){
        for(int i=0; i<buildings.Length; i++){
                if(GUI.Button(new Rect(0, Screen.height/15 + Screen.height/12, 100,30), buildings[i].name)){
                    buildingPlacement.SetItem(buildings[i]);
                }
            }
        if(GUI.Button(new Rect(0, Screen.height/15 + Screen.height/12, 100,30), "Close")){
                    builds = false;
                    menu = true;
                }
    }

    void BuildMenu(){
        if(GUI.Button(new Rect(0, Screen.height/15 + Screen.height/12, 100,30), "Build")){
                    menu = false;   
                    builds = true; 
                }
    }
}
