using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/PlayerController")]
public class PlayerControllerSettings : ScriptableObject
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpHeightY;
    [SerializeField] private float maxSpeed = 3f;

    public float MovementSpeed => movementSpeed;
    public float RotateSpeed => rotateSpeed;
    public float JumpHeightY => jumpHeightY;
    public float MaxSpeed => maxSpeed;
}

