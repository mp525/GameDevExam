using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour

{
InputManager inputManager;
PlayerMotion playerMotion;
CameraManager cameraManager;
public float hunger = 100f;
public float health = 100f;
[SerializeField] private float hungerOverTime = 100;




    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        cameraManager = FindObjectOfType<CameraManager>();

    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        HungerOverTime();

    }

    void HungerOverTime()
    {
        hunger -= (hungerOverTime * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerMotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
