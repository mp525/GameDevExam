using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour

{
    InputManager inputManager;
    public Transform targetTransform; //Objektet som kameraet til følge
    public Transform cameraPivot;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float lookAngle; //Kameraet kigger op og ned
    public float pivotAngle;//Kameraet kigger højre og venstre
    public float minimumPivotAngle = -35f;
    public float maximumPivotAngle = 35;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }
    private void FollowTarget()
    {
        //Denne vector3 variable, vil opdateres sig til at blive spillerens position (targetTransform) fra den nuværende position (transform)
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        //Sætter den akutelle position for spilleren
        transform.position = targetPosition;
        

    }


    private void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
