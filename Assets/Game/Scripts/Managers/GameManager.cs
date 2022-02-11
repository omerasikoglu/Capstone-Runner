using UnityEngine;
using System;

[Serializable]
public enum GameState
{
    None = 0,
    Starting = 1,
    TapToScreen = 6,
    Running = 2,
    Punch = 3,
    Flying = 4,
    Win = 5,
    Fail = 7
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<GameState> OnStateChanged;

    [field: SerializeField] public GameState State { get; private set; }

    private CameraHandler cameraHandler;
    private void Awake()
    {
        Instance ??= this;
        cameraHandler = GetComponent<CameraHandler>() ?? FindObjectOfType<CameraHandler>();

        
    }
    private void Start()
    {
        //Init Data
        PlayerPrefs.SetInt(StringData.PREF_MONEY, 0);
        UIManager.Instance.UpdateMoney();
        UIManager.Instance.UpdateLevel();

        PlayerPrefs.SetInt(StringData.PREF_LEVEL, 0);
        SceneLoadManager.Instance.LoadNextLevel();

        PlayerPrefs.SetInt(StringData.PREF_POINT, 0);

        Time.timeScale = 1f;

        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        if (State == newState) return;

        State = newState;

        switch (newState)
        {
            case GameState.Starting: HandleLoading(); break;
            case GameState.Running: HandleRunning(); break;
            case GameState.Punch: HandlePunch(); break;
            case GameState.Flying: HandleFlying(); break;
            case GameState.Win: HandleWin(); break;
            case GameState.None: break;
            case GameState.TapToScreen: HandleTapToScreen(); break;
            case GameState.Fail: HandleFail(); break;
            default: break;
        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleLoading()
    {
        UIManager.Instance.SwitchUI(GameUI.Loading);
        cameraHandler.SwitchCam(Cam.PreRunCam);

        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { ChangeState(GameState.TapToScreen); }, 2f));

    }
    private void HandleTapToScreen()
    {
        UIManager.Instance.SwitchUI(GameUI.TapToPlay);
    }
    private void HandleRunning()
    {
        cameraHandler.SwitchCam(Cam.RunningCam);
        UIManager.Instance.SwitchUI(GameUI.InGame);
    }

    private void HandleWin()
    {
        cameraHandler.SwitchCam(Cam.FinalPoseCam);
        UIManager.Instance.SwitchUI(GameUI.Win);
    }
    private void HandleFail()
    {
        cameraHandler.SwitchCam(Cam.FinalPoseCam);
        UIManager.Instance.SwitchUI(GameUI.Fail);
    }

    //TODO: Adjust Bottom 2
    private void HandlePunch()
    {
        //summon witch
        cameraHandler.SwitchCam(Cam.PunchCam);

    }
    private void HandleFlying()
    {
        // böyle bi þey yok
    }

    //from button
    public void SwitchToRunning()
    {
        ChangeState(GameState.Running);
    }
}
