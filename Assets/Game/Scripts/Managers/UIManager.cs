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
    [SerializeField, BoxGroup("[UI]")] private Transform tapToStartUI, youLoseUI, youWinUI, inGameUI, loadingUI;

    [SerializeField, BoxGroup("[Text Mesh Roots]")] private List<TextMeshProUGUI> moneyList, levelList;

    //[SerializeField] private TextMeshProUGUI textMesh;
    //[SerializeField] private TextMeshProUGUI textMeshScore;

    private List<Transform> uiList;

    private GameUI ui;


    protected void Awake()
    {
        Instance ??= this;
        uiList = new List<Transform>(5) { tapToStartUI, youLoseUI, youWinUI, inGameUI, loadingUI };
    }

    public void SwitchUI(GameUI newUI)
    {
        if (ui == newUI) return;

        ui = newUI;

        DisableAllUIs();

        switch (newUI)
        {
            case GameUI.None: break;
            case (GameUI)1: ChangeLoadingUI(); break;
            case (GameUI)2: ChangeTapToPlayUI(); break;
            case (GameUI)3: ChangeInGameUI(); break;
            case (GameUI)4: ChangeYouWinUI(); break;
            case (GameUI)5: ChangeGameOverUI(); break;
            default: break;
        }

    }
    public void UpdateLevel()
    {
        foreach (TextMeshProUGUI textMesh in levelList)
        {
            textMesh.SetText($"LEVEL {PlayerPrefs.GetInt(StringData.PREF_LEVEL)}");
        }
    }
    public void UpdateMoney()
    {
        foreach (TextMeshProUGUI textMesh in moneyList)
        {
            textMesh.SetText(PlayerPrefs.GetInt(StringData.PREF_MONEY).ToString());
        }
    }

    //UI
    private void DisableAllUIs()
    {
        foreach (Transform transform in uiList)
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
        UpdateMoney();
    }
    [Button]
    private void ChangeYouWinUI()
    {
        youWinUI.gameObject.SetActive(!youWinUI.gameObject.activeInHierarchy);
        UpdateMoney();
    }
    [Button]
    private void ChangeInGameUI()
    {
        inGameUI.gameObject.SetActive(!inGameUI.gameObject.activeInHierarchy);
        UpdateMoney();
    }

}