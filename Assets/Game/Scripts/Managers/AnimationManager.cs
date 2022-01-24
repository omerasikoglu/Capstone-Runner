using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance { get; private set; }

    [SerializeField] private Animator loadingGameUI;
    [SerializeField] private Animator introGameUI;
    [SerializeField] private Animator inGameUI;
    [SerializeField] private Animator winGameUI;
    [SerializeField] private Animator loseGameUI;

    private void Awake()
    {
        Instance = this;
    }

    [Button]
    public void ActivateLoadingGameUI()
    {
        loadingGameUI.SetBool(StringData.ISACTIVE, true);
    }
    [Button]
    public void DeactivateLoadingGameUI()
    {
        loadingGameUI.SetBool(StringData.ISACTIVE, false);
    }
    [Button]
    public void ActivateIntroGameUI()
    {
        introGameUI.SetBool(StringData.ISACTIVE, true);
    }
    [Button]
    public void DeactivateIntroGameUI()
    {
        introGameUI.SetBool(StringData.ISACTIVE, false);
    }
    [Button]
    public void ActivateInGameUI()
    {
        inGameUI.SetBool(StringData.ISACTIVE, true);
    }
    [Button]
    public void DeactivateInGameUI()
    {
        inGameUI.SetBool(StringData.ISACTIVE, false);
    }
    [Button]
    public void ActivateLoseGameUI()
    {
        loseGameUI.SetBool(StringData.ISACTIVE, true);
    }
    [Button]
    public void DeactivateLoseGameUI()
    {
        loseGameUI.SetBool(StringData.ISACTIVE, false);
    }
    [Button]
    public void ActivateWinGameUI()
    {
        winGameUI.SetBool(StringData.ISACTIVE, true);
    }
    [Button]
    public void DeactivateWinGameUI()
    {
        winGameUI.SetBool(StringData.ISACTIVE, false);
    }

}
