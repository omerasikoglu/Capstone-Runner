using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using System;

[Serializable]
public enum GameUI
{
    None = 0,
    Loading = 1,
    TapToPlay = 2,
    InGame = 3,
    Win = 4,
    Fail = 5,
}
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //private Transform youWinUI;
    [SerializeField] private Transform tapToStartUI;
    [SerializeField] private Transform youLoseUI;
    [SerializeField] private Transform youWinUI;
    [SerializeField] private Transform inGameUI;
    [SerializeField] private Transform loadingUI;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI textMeshScore;

    private List<Transform> UIList;
    private GameUI ui;


    protected void Awake()
    {
        Instance ??= this;
        UIList = new List<Transform>(5) { tapToStartUI, youLoseUI, youWinUI, inGameUI, loadingUI };

        //UpdateScore();
    }

    public void SwitchUI(GameUI newUI)
    {
        if (ui == newUI) return;

        ui = newUI;

        DisableAllUIs();

        switch (newUI)
        {
            case GameUI.None:
                break;
            case GameUI.Loading:
                ChangeLoadingUI();
                break;
            case GameUI.TapToPlay:
                ChangeTapToPlayUI();
                break;
            case GameUI.InGame:
                ChangeInGameUI();
                break;
            case GameUI.Win:
                ChangeYouWinUI();
                break;
            case GameUI.Fail:
                ChangeGameOverUI();
                break;
            default:
                break;
        }

    }
    public void UpdateScore()
    {
        textMesh.SetText(PlayerPrefs.GetInt(StringData.PREF_MONEY).ToString());
    }

    //UIs
    private void DisableAllUIs()
    {
        foreach (Transform transform in UIList)
        {
            transform.gameObject.SetActive(false);
        }
    }
   
    [Button]
    private void ChangeLoadingUI()
    {
        loadingUI.gameObject.SetActive(!loadingUI.gameObject.activeInHierarchy);
    }
    [Button]
    private void ChangeTapToPlayUI()
    {
        tapToStartUI.gameObject.SetActive(!tapToStartUI.gameObject.activeInHierarchy);
    }
    [Button]
    private void ChangeGameOverUI()
    {
        youLoseUI.gameObject.SetActive(!youLoseUI.gameObject.activeInHierarchy);
        textMeshScore.SetText("(Total Score:" + PlayerPrefs.GetInt(StringData.PREF_MONEY) + ")");
    }
    [Button]
    private void ChangeYouWinUI()
    {
        youWinUI.gameObject.SetActive(!youWinUI.gameObject.activeInHierarchy);
        textMeshScore.SetText("(Total Score:" + PlayerPrefs.GetInt(StringData.PREF_MONEY) + ")");
    }
    [Button]
    private void ChangeInGameUI()
    {
        inGameUI.gameObject.SetActive(!inGameUI.gameObject.activeInHierarchy);
    }

}