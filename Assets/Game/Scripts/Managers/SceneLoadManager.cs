using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public enum Level
{
    Level1 = 1, Level2 = 2, Level3 = 3
}
public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance { get; private set; }

    private bool isL1Loaded = false, isL2Loaded = false, isL3Loaded = false;

    private void Awake()
    {
        Instance ??= this;
    }
    public void LoadUnloadScene(Level newLevel)
    {
        if (CheckIsLoaded(newLevel)) UnloadScene(newLevel);
        else LoadScene(newLevel);
    }

    private bool CheckIsLoaded(Level newLevel)
    {
        return newLevel switch
        {
            (Level)1 => isL1Loaded,
            (Level)2 => isL2Loaded,
            (Level)3 => isL3Loaded,
            _ => false
        };
    }

    private void LoadScene(Level newLevel)
    {
        SetLevelBools(newLevel);
        SceneManager.LoadSceneAsync(GetSceneNameFromEnum(newLevel), LoadSceneMode.Additive);
    }

    private void UnloadScene(Level newLevel)
    {
        SetLevelBools(newLevel);
        SceneManager.UnloadSceneAsync(GetSceneNameFromEnum(newLevel));
    }
    private void SetLevelBools(Level newLevel)
    {
        switch (newLevel)
        {
            case Level.Level1: isL1Loaded = !isL1Loaded; break;
            case Level.Level2: isL2Loaded = !isL2Loaded; break;
            case Level.Level3: isL3Loaded = !isL3Loaded; break;
            default: break;
        }
    }
    private string GetSceneNameFromEnum(Level whichLevel)
    {
        return whichLevel switch
        {
            (Level)1 => StringData.LEVEL1,
            (Level)2 => StringData.LEVEL2,
            (Level)3 => StringData.LEVEL3,
            _ => StringData.LEVEL1
        };
    }

    //TEST

    [Button]
    private void level1()
    {
        LoadUnloadScene(Level.Level1);
    }
    [Button]
    private void level2()
    {
        LoadUnloadScene(Level.Level2);
    }
    [Button]
    private void level3()
    {
        LoadUnloadScene(Level.Level3);
    }
}
