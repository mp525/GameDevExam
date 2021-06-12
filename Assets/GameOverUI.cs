using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class GameOverUI : MonoBehaviour
{
    private void Start() {
        
    }
    public void PlayAgain(){
        SceneManager.LoadScene(1);
    }
    public  void Quit(){
         Application.Quit();
    }
}
