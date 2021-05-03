using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour

{
InputManager inputManager;
PlayerMotion playerMotion;
CameraManager cameraManager;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        cameraManager = FindObjectOfType<CameraManager>();

    }

    private void Update()
    {
        inputManager.HandleAllInputs();

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
