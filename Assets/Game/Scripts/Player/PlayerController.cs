using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSettings playerSettings;
    [SerializeField] private Transform slideMovementRoot, leftLimit, rightLimit;

    private Rigidbody rb => GetComponent<Rigidbody>();

    private Vector2 inputDrag, previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;

    private bool canMove = false;

    private void Update()
    {
        HandleInput();
        HandleSlideMovement();
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
    private void HandleSlideMovement()
    {
        var localPos = slideMovementRoot.localPosition;
        localPos += Vector3.right * inputDrag.x * playerSettings.SlideMovementSensitivity;

        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);

        slideMovementRoot.localPosition = localPos;

        var moveDirection = Vector3.forward * 0.5f;
        moveDirection += slideMovementRoot.right * inputDrag.x * playerSettings.SlideMovementSensitivity;

        moveDirection.Normalize();

        var targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        slideMovementRoot.rotation = Quaternion.Lerp(slideMovementRoot.rotation, targetRotation, Time.deltaTime * playerSettings.RotationSpeed);

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

    public void ChangeOutfit(bool isGoodItemTaken)
    {

    }
    public void ChangeOutfit()
    {
    }
}