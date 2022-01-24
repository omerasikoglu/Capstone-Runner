using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/PlayerController")]
public class PlayerControllerSettings : ScriptableObject
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeightY;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float slideMovementSensitivity = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    public float MovementSpeed => movementSpeed;
    public float RotationSpeed => rotationSpeed;
    public float SlideMovementSensitivity => slideMovementSensitivity;
    public float JumpHeightY => jumpHeightY;
    public float MaxSpeed => maxSpeed;
}

