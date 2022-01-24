using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSettings playerSettings;
    [SerializeField] private Transform sideMovementRoot, leftLimit, rightLimit;

    private Vector2 inputDrag, previousMousePosition;

    private Rigidbody rb => GetComponent<Rigidbody>();
    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;

    private bool canMove = false;
    
    private void Update()
    {
        HandleInput();
        HandleSideMovement();
        CheckMovement();
    }
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }
    private void HandleSideMovement()
    {
        var localPos = sideMovementRoot.localPosition;
        localPos += Vector3.right * inputDrag.x * playerSettings.SlideMovementSensitivity;

        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);

        sideMovementRoot.localPosition = localPos;

        var moveDirection = Vector3.forward * 0.5f;
        moveDirection += sideMovementRoot.right * inputDrag.x * playerSettings.SlideMovementSensitivity;

        moveDirection.Normalize();

        var targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        sideMovementRoot.rotation = Quaternion.Lerp(sideMovementRoot.rotation, targetRotation, Time.deltaTime * playerSettings.RotationSpeed);

    }
    private void CheckMovement()
    {
        if (!canMove && InputManager.IsClickDownAnything)
        {
            //first move
            canMove = !canMove;
            UIManager.Instance.SetDeactiveTapToStartUI();
        }
        if (canMove)
        {
            //TODO: RUN
        }
    }

}