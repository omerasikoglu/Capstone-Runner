using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/PlayerController")]
public class PlayerControllerSettings : ScriptableObject
{

    [SerializeField] private float movementSpeed;
    public float MovementSpeed => movementSpeed;
   
    [SerializeField] private float jumpHeightY;
    public float JumpHeightY => jumpHeightY;
    
    [SerializeField] private float maxSpeed = 3f;
    public float MaxSpeed => maxSpeed;
   
    [SerializeField] private float slideMovementSensitivity = 1f;
    public float SlideMovementSensitivity => slideMovementSensitivity;
   
    [SerializeField] private float rotationSpeed = 1f;
    public float RotationSpeed => rotationSpeed;

    [SerializeField] private int outfitChangeLimit1, outfitChangeLimit2, outfitChangeLimit3;

    public int GoodOutfitChangeLimit1 => outfitChangeLimit1;
    public int GoodOutfitChangeLimit2 => outfitChangeLimit2;
    public int GoodOutfitChangeLimit3 => outfitChangeLimit3;
    public int BadOutfitChangeLimit1 => (outfitChangeLimit1) * -1;
    public int BadOutfitChangeLimit2 => (outfitChangeLimit2) * -1;
    public int BadOutfitChangeLimit3 => (outfitChangeLimit3) * -1;
}

