using UnityEngine;
using System;

[Serializable]
public enum GameState
{
    None = 0,
    Starting = 1,
    TapToScreen = 6,
    Running = 2,
    Kicking = 3,
    Flying = 4,
    Win = 5
}
public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnStateChanged;

    [field: SerializeField] public GameState State { get; private set; }

    private CameraHandler cameraHandler;
    private void Awake() => cameraHandler ??= GetComponent<CameraHandler>() ?? FindObjectOfType<CameraHandler>();
    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        if (State == newState) return;

        State = newState;

        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Running:
                HandleRunning();
                break;
            case GameState.Kicking:
                HandleMinigame();
                break;
            case GameState.Flying:
                HandleFlying();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.None:
                break;
            case GameState.TapToScreen:
                HandleTapToScreen();
                break;
            default:
                break;
        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleStarting()
    {
        //TODO: UI loading screen, some vCam cinematics
        Time.timeScale = 1f;

        //open
        UIManager.Instance.ChangeLoadingUI();

        //close
        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => 
        { UIManager.Instance.ChangeLoadingUI(); ChangeState(GameState.TapToScreen); }, 2f));

    }
    private void HandleTapToScreen()
    {
        UIManager.Instance.ChangeTapToStartUI();
    }
    private void HandleRunning()
    {
        //Close
        UIManager.Instance.ChangeTapToStartUI();

        //Play
        cameraHandler.SwitchCam(Cam.RunningCam);

    }
    private void HandleMinigame()
    {
        ChangeState(GameState.Kicking);
        cameraHandler.SwitchCam(Cam.MinigameCam);

        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { ChangeState(GameState.Flying); }, 5f));
    }
    private void HandleWin()
    {
        ChangeState(GameState.Flying);
    }
    private void HandleFlying()
    {
        cameraHandler.SwitchCam(Cam.FinalPoseCam);
    }

    public void SwitchToRunning()
    {
        ChangeState(GameState.Running);
    }
}
