using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/PlayerController")]
public class PlayerControllerSettings : ScriptableObject
{

    public float movementSpeed;
   
    [SerializeField] private float jumpHeightY;
    public float JumpHeightY => jumpHeightY;
    
    [SerializeField] private float maxSpeed = 3f;
    public float MaxSpeed => maxSpeed;
   
    [SerializeField] private float slideMovementSensitivity = 1f;
    public float SlideMovementSensitivity => slideMovementSensitivity;
   
    [SerializeField] private float rotationSpeed = 1f;
    public float RotationSpeed => rotationSpeed;

    [SerializeField] private int outfitPoint;
    public int OutfitPoint => outfitPoint;

    [SerializeField] private int itemPointLimit1, itemPointLimit2, itemPointLimit3;
    public int GoodItemPointLimit1 => itemPointLimit1;
    public int GoodItemPointLimit2 => itemPointLimit2;
    public int GoodItemPointLimit3 => itemPointLimit3;
    public int BadItemPointLimit1 => (itemPointLimit1) * -1;
    public int BadItemPointLimit2 => (itemPointLimit2) * -1;
    public int BadItemPointLimit3 => (itemPointLimit3) * -1;
}

