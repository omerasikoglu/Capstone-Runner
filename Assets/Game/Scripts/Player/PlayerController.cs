using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField, BoxGroup("[Punched Guy]")] private GameObject guy;
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
    private Vector3 GuyTransformFirst;

    private void Awake()
    {
        //Init
        animatorList = new List<Animator>() { witchAnimator, premsesAnimator, flatWomanAnimator };

        GuyTransformFirst = guy.transform.position;
        //Reset attığımızda eski konumuna gelsin diye
        Debug.Log("guyTransformFirst = " + GuyTransformFirst);
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
                StartIdle();
                break;
            case GameState.Running:
                StartRun();
                break;
            case GameState.WatchPunchedGuy:
                StartPunchedGuyFlying();
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
                ActivateFlatWoman();

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

            PlayerPrefs.SetInt(StringData.PREF_POINT, currentPoint);
            PointBarUI.Instance.SetTargetItemRectAmount();

            PlayFX(princessItemTakeFX);
        }
        else if (isPrincessItem == false)
        {
            if (areYouPrincess == null) return;

            currentPoint += areYouPrincess == false ? 1 : -1;
            currentPoint = Mathf.Clamp(currentPoint, 0, 8000);

            PlayerPrefs.SetInt(StringData.PREF_POINT, currentPoint);
            PointBarUI.Instance.SetTargetItemRectAmount();

            PlayFX(witchItemTakeFX);
        }

        else if (isPrincessItem == null)
        {
            //altın aldıysa
            currentMoney += 10;

            PlayerPrefs.SetInt(StringData.PREF_MONEY, currentMoney);
            UIManager.Instance.UpdateMoney();

            PlayFX(moneyTakeFX, 1);
        }

        void PlayFX(ParticleSystem particle, int isOldEmitPending = 0)
        {
            princessItemTakeFX.Stop(true, (ParticleSystemStopBehavior)isOldEmitPending);
            witchItemTakeFX.Stop(true, (ParticleSystemStopBehavior)isOldEmitPending);
            moneyTakeFX.Stop(true, (ParticleSystemStopBehavior)isOldEmitPending);
            if (particle != null) particle.Play();
        }

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
        }, 0.5f));
    }

    [Button]
    public void ResetPlayer()
    {
        gameObject.transform.position = new Vector3(0f, 0f, -10f);
        guy.transform.position = GuyTransformFirst;
        Debug.Log(" guy.transform.position = " + guy.transform.position);

        ActivateFlatWoman();
        StartIdle();

        //Reset data
        currentOutfitPoint = 0; currentOutfitPoint = 0; currentPoint = 0;
        PlayerPrefs.SetInt(StringData.PREF_MONEY, currentMoney);
        PlayerPrefs.SetInt(StringData.PREF_POINT, currentPoint);

        GameManager.Instance.ChangeState(GameState.TapToScreen);

        UIManager.Instance.UpdateMoney();
        UIManager.Instance.UpdateLevel();
        PointBarUI.Instance.ResetBar();

        familiarController.ResetFamiliars();
        gateBarUI.ResetBar();
    }
    private void ActivateFlatWoman()
    {
        witchAnimator.gameObject.SetActive(false);
        premsesAnimator.gameObject.SetActive(false);
        flatWomanAnimator.gameObject.SetActive(true);
        UpdateAreYouPrincess();
    }
    public bool? GetAreYouPrincess()
    {
        return areYouPrincess;
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
        guy.transform.DOLocalRotate(new Vector3(0f, guy.transform.localRotation.y, guy.transform.localRotation.z), 0f);
    }
    [Button]
    public void StartRun()
    {
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.RUNNING);
        }
        SetCharacterSpeed(playerSettings.MaxMovementSpeed);
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
        SetCharacterSpeed(0f);
        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.activeInHierarchy) animator.SetTrigger(StringData.PUNCH);
        }

        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() =>
        {
            GameManager.Instance.ChangeState(GameState.WatchPunchedGuy);
        }, 1f));

        guy.gameObject.SetActive(true);
    }
    private void StartPunchedGuyFlying()
    {
        float kickDistance = Mathf.InverseLerp(0f, 20f, PlayerPrefs.GetInt(StringData.PREF_POINT)) * 18f; //20 birim
        guy.transform.DOLocalMoveZ(guy.transform.localPosition.z + kickDistance, 4f).SetEase(Ease.OutCirc);
        PointBarUI.Instance.ResetBar();

        guy.transform.DOLocalRotate(new Vector3(90f, guy.transform.localRotation.y, guy.transform.localRotation.z), 2f);

        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() =>
        {
            GameManager.Instance.ChangeState(GameState.Win);
        }, 5f));
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