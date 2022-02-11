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
    PunchCam = 2,
    FinalPoseCam = 3,
    PreRunCam = 4,
    FinalScoreCam = 5,
}

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam1, vCam2, vCam3, vCam4, vCam5;

    private Cam oldCam;
    private CinemachineVirtualCamera currentCam;
    private List<CinemachineVirtualCamera> camList;

    private void Awake() => camList = new List<CinemachineVirtualCamera>(5) { vCam1, vCam2, vCam3, vCam4, vCam5 };

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
            (Cam)5 => vCam5,
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
