using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform inGameUI;

    private void Awake()
    {
        inGameUI.gameObject.SetActive(false);
    }
    private void Start()
    {
        AnimationManager.Instance.ActivateLoadingGameUI();
    }

}
