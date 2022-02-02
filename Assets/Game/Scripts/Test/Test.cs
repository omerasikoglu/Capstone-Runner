using UnityEngine;
using NaughtyAttributes;

public class Test : MonoBehaviour
{
    int times = 0;

    private void Awake() => GameManager.OnStateChanged += GameManager_OnBeforeStateChanged;
    private void OnDestroy() => GameManager.OnStateChanged -= GameManager_OnBeforeStateChanged;

    private void GameManager_OnBeforeStateChanged(GameState obj)
    {
        if (obj == GameState.Running) Debug.Log($"stateChanged {++times} times");

    }




}
