using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public void PrintEvent(string s) 
    {
        Debug.Log("Hit: " + s + " called at: " + Time.time);
    }

}
