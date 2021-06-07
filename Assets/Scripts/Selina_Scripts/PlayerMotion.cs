using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    Vector3 moveDirection;
    Transform cameraObject;
    InputManager inputManager;
    Rigidbody playerRB;

    public float movementSpeed = 7;
    public float rotationSpeed = 15;
    public Animator anim;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cameraObject = Camera.main.transform;
        anim.SetFloat("Speed", 0.0f);
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }


    
    private void HandleMovement()
    {
    //Hvis moveDirections værdi er mindre end 0.1, så skal animationen sættes til at være "Idle"
        if(moveDirection.magnitude < 0.1f)
        {
            anim.SetFloat("Speed", 0.0f);
        }


        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;
        

        Debug.Log("Magnitude : " + moveDirection.magnitude);

        Vector3 movementVelocity = moveDirection;
        playerRB.velocity = movementVelocity;

    //Hvis moveDirections værdi er større end 0.1, så skal animationen sættes til at være "Run"
        if(moveDirection.magnitude > 0.1f)
        {
            anim.SetFloat("Speed", 7f);
        }
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;

    }
}
