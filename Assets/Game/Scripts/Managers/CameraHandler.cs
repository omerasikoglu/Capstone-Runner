using UnityEngine;
using NaughtyAttributes;
using Cinemachine;
using System.Collections.Generic;
using System;

[Serializable]
public enum Cam
{
    None = 0,
    RunningCam = 1,
    MinigameCam = 2,
    FinalPoseCam = 3,
    BeforeRunningCam = 4
}

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam1, vCam2, vCam3, vCam4;

    private Cam oldCam;
    private CinemachineVirtualCamera currentCam;
    private List<CinemachineVirtualCamera> camList;

    private void Awake() => camList = new List<CinemachineVirtualCamera>(4) { vCam1, vCam2, vCam3, vCam4 };

    public void SwitchCam(Cam newCam)
    {
        if (oldCam == newCam) return;

        oldCam = newCam;

        currentCam = newCam switch
        {
            (Cam)1 => vCam1,
            (Cam)2 => vCam2,
            (Cam)3 => vCam3,
            (Cam)4 => vCam4,
            _ => vCam1
        };

        ActivateNewCam(currentCam);
    }

    private void ActivateNewCam(CinemachineVirtualCamera newCam)
    {
        foreach (CinemachineVirtualCamera cam in camList)
        {
            cam.gameObject.SetActive(false);
        }
        newCam.gameObject.SetActive(true);
    }

    #region TEST

    [Button]
    private void activateVCam1()
    {
        foreach (CinemachineVirtualCamera cam in camList)
        {
            cam.gameObject.SetActive(false);
        }
        vCam1.gameObject.SetActive(true);
    }
    [Button]
    private void activateVCam2()
    {
        foreach (CinemachineVirtualCamera cam in camList)
        {
            cam.gameObject.SetActive(false);
        }
        vCam2.gameObject.SetActive(true);
    }
    [Button]
    private void activateVCam3()
    {
        foreach (CinemachineVirtualCamera cam in camList)
        {
            cam.gameObject.SetActive(false);
        }
        vCam3.gameObject.SetActive(true);
    }

    #endregion

}
