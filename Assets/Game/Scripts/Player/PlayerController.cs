using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    public float characterSpeed = 5f;
    [SerializeField, BoxGroup("[Settings]")] private PlayerControllerSettings playerSettings;
    [SerializeField, BoxGroup("[Settings]")] private FamiliarController familiarController;
    [SerializeField, BoxGroup("[FXs]")] private ParticleSystem goodGatePassFX, badGatePassFX, goodItemTakeFX, badItemTakeFX;
    [SerializeField, BoxGroup("[Outfits]")] private Transform defaultOutfit;
    [SerializeField, BoxGroup("[Outfits]")] private Transform[] goodOutfitArray = new Transform[3], badOutfitArray = new Transform[3];

    #region Movement
    [SerializeField, BoxGroup("[Move]")] private Transform slideMovementRoot, leftLimit, rightLimit;

    private Vector2 inputDrag, previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;

    private bool canMove = true;
    #endregion

    private int currentItemPoint = 0, currentOutfitPoint = 0;

    private void Start()
    {
        ResetOutfits();
    }
    private void Update()
    {
        HandleInput();
        HandleSlideMovement();
        CheckMovement();
    }

    private void ResetOutfits()
    {
        defaultOutfit.gameObject.SetActive(true);

        foreach (Transform outfit in goodOutfitArray)
        {
            outfit.gameObject.SetActive(false);
        }
        foreach (Transform outfit in badOutfitArray)
        {
            outfit.gameObject.SetActive(false);
        }
    }

    #region Movement
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
    private void HandleMovement()
    {
        transform.position += transform.forward * Time.deltaTime * characterSpeed;

    }
    private void CheckMovement()
    {
        if (!canMove && InputManager.IsClickDownAnything)
        {
            //first move
            canMove = !canMove;
            UIManager.Instance.ChangeTapToStartUI();
        }
        if (canMove)
        {
            //TODO: RUN
            HandleMovement();

        }
    }
    #endregion

    public void ChangeOutfit(bool isGoodGateHasBeenPassed)
    {
        currentOutfitPoint += isGoodGateHasBeenPassed ? 1 : -1;

        //kıyafet puanına göre karakterin kıyafetini değiştirme
        switch (currentOutfitPoint)
        {
            case -3:
                badOutfitArray[0].gameObject.SetActive(true);
                badOutfitArray[1].gameObject.SetActive(true);
                badOutfitArray[2].gameObject.SetActive(true);
                break;
            case -2:
                badOutfitArray[0].gameObject.SetActive(true);
                badOutfitArray[1].gameObject.SetActive(true);
                badOutfitArray[2].gameObject.SetActive(false);
                break;
            case -1:
                badOutfitArray[0].gameObject.SetActive(true);
                badOutfitArray[1].gameObject.SetActive(false);
                break;
            case 0:
                badOutfitArray[0].gameObject.SetActive(false);
                goodOutfitArray[0].gameObject.SetActive(false);
                break;
            case 1:
                goodOutfitArray[0].gameObject.SetActive(true);
                goodOutfitArray[1].gameObject.SetActive(false);
                break;
            case 2:
                goodOutfitArray[0].gameObject.SetActive(true);
                goodOutfitArray[1].gameObject.SetActive(true);
                goodOutfitArray[2].gameObject.SetActive(false);
                break;
            case 3:
                goodOutfitArray[0].gameObject.SetActive(true);
                goodOutfitArray[1].gameObject.SetActive(true);
                goodOutfitArray[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
        if (isGoodGateHasBeenPassed)
        {
            goodGatePassFX.Play();
            familiarController.AddNewFamiliar();
        }
        else
        {
            badGatePassFX.Play();
            familiarController.RemoveOldFamiliar();
        }
    }
    public void ChangeItemPoint(bool isGoodItem)
    {
        currentItemPoint += isGoodItem ? 1 : -1;
    }

    #region Test
    [Button] void addGood() { ChangeOutfit(true); }
    [Button] void addBad() { ChangeOutfit(false); } 
    #endregion
}