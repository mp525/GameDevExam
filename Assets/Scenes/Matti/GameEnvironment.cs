using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment
{
    public static GameEnvironment instance;
    private List<GameObject>checkpoints=new List<GameObject>();
    public List<GameObject>Checkpoints{get{return checkpoints;}}
    // Start is called before the first frame update
    public static GameEnvironment Singleton
    {
        get{
            if (instance==null)
            {
                instance=new GameEnvironment();
                instance.Checkpoints.AddRange(
                    GameObject.FindGameObjectsWithTag("Checkpoint"));
                
            }
            return instance;
        }
    }
}
