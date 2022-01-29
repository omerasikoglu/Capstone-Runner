using UnityEngine;
using System;

[Serializable]
public enum GameState
{
    None = 0,
    Starting = 1,
    Running = 2,
    Minigame = 3,
    Flying = 4,
    Win = 5,
}
public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

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
        Time.timeScale = 1f;

        //Debug.Log("started");
        ChangeState(GameState.Running);
    }
    private void HandleRunning()
    {
        // Run run
    }
    private void HandleMinigame()
    {
        //Stop movement, Inputs changes, cameras changes 2 times at first and last

        ChangeState(GameState.Running);
    }

    private void HandleWin()
    {
        //Stop Moving, Trigger Win anims

        ChangeState(GameState.Flying);
    }

    private void HandleFlying()
    {
        //Flying
    }

}
