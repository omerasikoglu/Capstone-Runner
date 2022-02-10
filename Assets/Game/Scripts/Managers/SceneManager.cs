using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public enum Level
{
    Level1 = 1, Level2 = 2, Level3 = 3
}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    private bool isL1Loaded = false, isL2Loaded = false, isL3Loaded = false;

    private void Awake()
    {
        Instance ??= this;
    }

    [Button]
    public void ChangeL1State()
    {
        if (isL1Loaded)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Level1");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
        }
        isL1Loaded = !isL1Loaded;
    }
    public void LoadScene(Level newLevel)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(GetSceneNameFromEnum(newLevel), LoadSceneMode.Additive);
    }
    public void UnloadScene(Level newLevel)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(GetSceneNameFromEnum(newLevel));
    }
    private string GetSceneNameFromEnum(Level whichLevel)
    {
        return whichLevel switch
        {
            (Level)1 => "Level1",
            (Level)2 => "Level2",
            (Level)3 => "Level3",
            _ => "Level1"
        };
    }
}
