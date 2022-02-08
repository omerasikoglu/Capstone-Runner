using UnityEngine;
using System;
using System.Collections;

[Serializable]
public enum GameState
{
    None = 0,
    Starting = 1,
    Running = 2,
    Minigame = 3,
    Flying = 4,
    Win = 5
}
public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    [field: SerializeField] public GameState State { get; private set; }

    private CameraHandler cameraHandler;
    private void Awake() => cameraHandler ??= GetComponent<CameraHandler>() ?? FindObjectOfType<CameraHandler>();
    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        if (State == newState) return;

        OnBeforeStateChanged?.Invoke(newState);

        State = newState;

        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Running:
                HandleRunning();
                break;
            case GameState.Minigame:
                HandleMinigame();
                break;
            case GameState.Flying:
                HandleFlying();
                break;
            case GameState.Win:
                HandleWin();
                break;
            default:
                break;
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private void HandleStarting()
    {
        //TODO: UI loading screen, some vCam cinematics

        UIManager.Instance.ChangeLoadingUI();
        StartCoroutine(WaitCertainAmountOfTime(() => { UIManager.Instance.ChangeLoadingUI(); }, 1f));

        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Running); }, 3f));
    }
    private void HandleRunning()
    {
        // Run run
        Time.timeScale = 1f;

        cameraHandler.SwitchCam(Cam.RunningCam);

        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Minigame); }, 4f));
    }
    private void HandleMinigame()
    {
        //Stop movement, Inputs changes, cameras changes 2 times at first and last

        ChangeState(GameState.Minigame);
        cameraHandler.SwitchCam(Cam.MinigameCam);
        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Flying); }, 5f));
    }

    private void HandleWin()
    {
        //Stop Moving, Trigger Win anims

        ChangeState(GameState.Flying);
    }

    private void HandleFlying()
    {
        //Flying
        cameraHandler.SwitchCam(Cam.FinalPoseCam);
    }

    private IEnumerator WaitCertainAmountOfTime(Action action, float secs)
    {
        yield return new WaitForSeconds(secs);
        action();
    }
}
