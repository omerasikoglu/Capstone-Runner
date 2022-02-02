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
    public static event Action<GameState> OnStateChanged;

    [field: SerializeField] public GameState State { get; private set; }

    private CameraHandler cameraHandler;
    private void Awake()
    {
        cameraHandler = GetComponent<CameraHandler>() ?? FindObjectOfType<CameraHandler>();

        //PlayerPrefs
        PlayerPrefs.SetInt(StringData.MONEY, 0);
        PlayerPrefs.SetInt(StringData.LEVEL, 1);
    }
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

        OnStateChanged?.Invoke(newState);
    }

    private void HandleStarting()
    {
        //TODO: UI loading screen, some vCam cinematics
        Time.timeScale = 1f;

        StartCoroutine(WaitCertainAmountOfTime(() => { HandleBeforeRunning(); }, 1f));
    }
    private void HandleBeforeRunning()
    {
        cameraHandler.SwitchCam(Cam.BeforeRunningCam);

        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Minigame); }, 1f));
    }
    private void HandleRunning()
    {
        // Run run
        cameraHandler.SwitchCam(Cam.RunningCam);

        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Minigame); }, 3f));
    }
    private void HandleMinigame()
    {
        //Stop movement, Inputs changes, cameras changes 2 times at first and last

        cameraHandler.SwitchCam(Cam.MinigameCam);

        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Flying); }, 5f));
    }

    private void HandleFlying()
    {
        //Flying
        cameraHandler.SwitchCam(Cam.FinalPoseCam);

        StartCoroutine(WaitCertainAmountOfTime(() => { ChangeState(GameState.Running); }, 3f));
    }
    private void HandleWin()
    {
        //Stop Moving, Trigger Win anims

    }
    private IEnumerator WaitCertainAmountOfTime(Action action, float secs = 0f)
    {
        yield return new WaitForSeconds(secs);
        action();
    }
}
