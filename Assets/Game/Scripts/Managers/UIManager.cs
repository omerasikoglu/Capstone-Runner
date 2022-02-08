using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

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



    protected void Awake()
    {
        Instance ??= this;
        UIList = new List<Transform>(5) { tapToStartUI, youLoseUI, youWinUI, inGameUI, loadingUI };

        //UpdateScore();
    }


    public void UpdateScore()
    {
        textMesh.SetText(PlayerPrefs.GetInt(StringData.MONEY).ToString());
    }

    //UIs
    [Button]
    public void ChangeLoadingUI()
    {
        loadingUI.gameObject.SetActive(!loadingUI.gameObject.activeInHierarchy);
    }
    [Button]
    public void ChangeTapToStartUI()
    {
        tapToStartUI.gameObject.SetActive(!tapToStartUI.gameObject.activeInHierarchy);
    }
    [Button]
    public void ChangeGameOverUI()
    {
        youLoseUI.gameObject.SetActive(!youLoseUI.gameObject.activeInHierarchy);
        textMeshScore.SetText("(Total Score:" + PlayerPrefs.GetInt(StringData.MONEY) + ")");
    }
    [Button]
    public void ChangeYouWinUI()
    {
        youWinUI.gameObject.SetActive(!youWinUI.gameObject.activeInHierarchy);
        textMeshScore.SetText("(Total Score:" + PlayerPrefs.GetInt(StringData.MONEY) + ")");
    }
    [Button]
    public void ChangeInGameUI()
    {
        inGameUI.gameObject.SetActive(!inGameUI.gameObject.activeInHierarchy);
    }
  

    private void DisableAllUIs()
    {
        foreach (Transform transform in UIList)
        {
            transform.gameObject.SetActive(false);
        }
    }

}