using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void PlayAgain(){
        SceneManager.LoadScene(1);
    }
    public  void Quit(){
         Application.Quit();
    }
}
