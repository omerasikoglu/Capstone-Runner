using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField, BoxGroup("[Animator]")] private Animator witchAnimator, premsesAnimator, flatWomanAnimator;
    [SerializeField, BoxGroup("[Settings]")] private PlayerControllerSettings playerSettings;
    [SerializeField, BoxGroup("[Settings]")] private FamiliarController familiarController;
    [SerializeField, BoxGroup("[Settings]")] private List<BoxCollider> colliderList;
    [SerializeField, BoxGroup("[UI]")] private GateBarUI gateBarUI;
    [SerializeField, BoxGroup("[FXs]")] private ParticleSystem trueGateIsPassedFX, wrongGateIsPassedFX, princessItemTakeFX, witchItemTakeFX, moneyTakeFX;
    [SerializeField, BoxGroup("[Outfits]")] private Transform[] goodOutfitArray = new Transform[3], badOutfitArray = new Transform[3];


    private List<Animator> animatorList;
    private bool? areYouPrincess = null;

    private int currentMoney = 0, currentOutfitPoint = 0, currentPoint = 0;

    private void Awake()
    {
        //Init
        animatorList = new List<Animator>() { witchAnimator, premsesAnimator, flatWomanAnimator };

        UpdateAreYouPrincess();
    }
    private void Start()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        ResetOutfits();
    }

    private void UpdateAreYouPrincess()
    {
        if (witchAnimator.gameObject.activeSelf)
        {
            areYouPrincess = false;
        }
        else if (premsesAnimator.gameObject.activeSelf)
        {
            areYouPrincess = true;
        }
        else areYouPrincess = null;
    }
    #region Movement
    [SerializeField, BoxGroup("[Move]")] private Transform slideMovementRoot, leftLimit, rightLimit;

    private Vector2 inputDrag, previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;

    private bool canMove = true;
    #endregion




    private void GameManager_OnStateChanged(GameState obj)
    {
        Debug.Log(obj);
        switch (obj)
        {
            case GameState.None:
                break;
            case GameState.Starting:
                StartIdle();
                break;
            case GameState.TapToScreen:
                break;
            case GameState.Running:
                StartRun();
                break;
            case GameState.Flying:
                break;
            case GameState.Win:
                StartWin();
                break;
            case GameState.Punch:
                StartPunch();
                break;
            case GameState.Fail:
                StartFail();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        HandleInput();
        HandleSlideMovement();
        CheckMovement();
    }

    private void ResetOutfits()
    {

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
        transform.position += transform.forward * Time.deltaTime * playerSettings.movementSpeed;

    }
    private void CheckMovement()
    {
        if (!canMove && InputManager.IsClickDownAnything)
        {
            //first move
            canMove = !canMove;
        }
        if (canMove)
        {
            HandleMovement();
        }
    }
    #endregion

    private void SetCharacterSpeed(float speed)
    {
        if (playerSettings.movementSpeed == speed) return;

        playerSettings.movementSpeed = speed;
    }

    //public
    public void GateIsPassed(bool isPrincessGate)
    {
        SetColliderDisableOneSec(); //aynı anda 2 kapıdan geçerse diye

        ChangeFamiliar(isPrincessGate);
        ChangeOutfit(isPrincessGate);
        StartSpin();
    }
    private void ChangeFamiliar(bool isPrincessGate)
    {
        if (isPrincessGate)
        {
            if (areYouPrincess == true || areYouPrincess == null)
            {
                gateBarUI.OneTaskDone(true);
                trueGateIsPassedFX.Play();
                familiarController.AddNewButterfly();
            }
            else
            {
                gateBarUI.OneTaskDone(false);
                wrongGateIsPassedFX.Play();
                familiarController.RemoveOldFamiliar();
            }
        }
        else if (!isPrincessGate)
        {
            if (areYouPrincess == false || areYouPrincess == null)
            {
                gateBarUI.OneTaskDone(true);
                trueGateIsPassedFX.Play();
                familiarController.AddNewBat();
            }
            else
            {
                gateBarUI.OneTaskDone(false);
                wrongGateIsPassedFX.Play();
                familiarController.RemoveOldFamiliar();
            }
        }
    }
    private void ChangeOutfit(bool isPrincessGate)
    {
        currentOutfitPoint += isPrincessGate ? 1 : -1;

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
                flatWomanAnimator.gameObject.SetActive(false);
                witchAnimator.gameObject.SetActive(true);
                UpdateAreYouPrincess();

                badOutfitArray[0].gameObject.SetActive(true);
                badOutfitArray[1].gameObject.SetActive(false);
                break;
            case 0:
                witchAnimator.gameObject.SetActive(false);
                premsesAnimator.gameObject.SetActive(false);
                flatWomanAnimator.gameObject.SetActive(true);
                UpdateAreYouPrincess();

                badOutfitArray[0].gameObject.SetActive(false);
                goodOutfitArray[0].gameObject.SetActive(false);
                break;
            case 1:
                flatWomanAnimator.gameObject.SetActive(false);
                premsesAnimator.gameObject.SetActive(true);
                UpdateAreYouPrincess();

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
    }

    public void SomeCollectibleHasTaken(bool? isPrincessItem)
    {
        if (isPrincessItem == true)
        {
            if (areYouPrincess == null) return;

            currentPoint += areYouPrincess == true ? 1 : -1;
            currentPoint = Mathf.Clamp(currentPoint, 0, 8000);

            //TODO: Update UI
            princessItemTakeFX.Play();
        }
        else if (isPrincessItem == false)
        {
            if (areYouPrincess == null) return;

            currentPoint += areYouPrincess == false ? 1 : -1;
            currentPoint = Mathf.Clamp(currentPoint, 0, 8000);

            //TODO: Update UI
            witchItemTakeFX.Play();
        }

        else if (isPrincessItem == null)
        {
            //altın aldıysa
            currentMoney += 10;
            PlayerPrefs.SetInt(StringData.PREF_MONEY, currentMoney);
            UIManager.Instance.UpdateMoney();

            moneyTakeFX.Play();
        }
        PlayerPrefs.SetInt(StringData.PREF_POINT, currentPoint);


    }

    private void SetColliderDisableOneSec()
    {
        foreach (BoxCollider collider in colliderList)
        {
            collider.enabled = false;
        }
        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() =>
        {
            foreach (BoxCollider collider in colliderList)
            {
                collider.enabled = true;
            }
        }, 1f));
    }


    #region Animations
    [Button]
    public void StartIdle()
    {
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.IDLE);
        }
        SetCharacterSpeed(0f);
    }
    [Button]
    public void StartRun()
    {
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.RUNNING);
        }
        SetCharacterSpeed(5f);
    }
    [Button]
    public void StartSpin()
    {
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.SPIN);
        }

        if (areYouPrincess == null)
        {
            //düz kadının spin animasyonu yok o yüzden
            StartRun();
        }
        else
        {
            StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { StartRun(); }, 1f));
        }
    }
    [Button]
    public void StartFall()
    {
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.FALL);
        }
    }
    [Button]
    public void StartPunch()
    {
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.PUNCH);
        }
    }
    [Button]
    public void StartFail()
    {
        SetCharacterSpeed(0f);
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.IDLE);
        }

        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() =>
        {
            foreach (Animator animator in animatorList)
            {
                if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.FAIL);
            };
        }, 2f));
    }
    [Button]
    public void StartWin()
    {
        SetCharacterSpeed(0f);
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.IDLE);
        }

        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() =>
        {
            foreach (Animator animator in animatorList)
            {
                if (animator.gameObject.activeInHierarchy)
                {
                    animator.speed = 0.5f;
                    animator.SetTrigger(StringData.SPIN);
                }

            }
        }, 2f));


    }

    #endregion

    #region Test
    [Button] void addGood() { GateIsPassed(true); }
    [Button] void addBad() { GateIsPassed(false); }
    #endregion
}