using UnityEngine;
using Unity.Collections;
using NaughtyAttributes;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;

public class GateBarUI : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 2f; //yeþil bar dolma hareket hýzý

    [SerializeField] private RectTransform barTransform; //MAX 375
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Canvas canvas;

    private float targetRect;
    private float barDisappearTime = 6f;
    private int currentProgress = 0; //yapýlan görev
    private int maxProgress = 3; //toplam görev

    private void Awake()
    {
        ResetBar();
    }

    private void Start()
    {
        SetTargetProgressRectAmount(currentProgress);
    }
    private void Update()
    {
        CheckGreenBar();
    }
    private void HideBar()
    {
        canvas.gameObject.SetActive(false);
    }
    private void CheckGreenBar()
    {
        //yeþil barýn görev tamamlandýkça yavaþça artmasý
        barTransform.sizeDelta = new Vector2(
            Mathf.Lerp(barTransform.sizeDelta.x, GetTargetProgressAmount(), lerpSpeed * Time.deltaTime), 44f);
    }
    public void OneTaskDone(bool isIncreased)
    {
        if (isIncreased)
        {
            currentProgress += 1;
            //if (currentProgress > maxProgress) currentProgress = 0;
        }
        else
        {
            currentProgress -= 1;
        }
        currentProgress = Mathf.Clamp(currentProgress, 0, maxProgress);

        SetTargetProgressRectAmount(currentProgress);
        SetWritings(currentProgress);
        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { HideBar(); }, barDisappearTime));
    }

    public void ResetBar()
    {
        currentProgress = 0;

        SetTargetProgressRectAmount(currentProgress);
    }
    private float GetTargetProgressAmount()
    {
        return targetRect;
    }
    private void SetWritings(int currentProgress)
    {
        textMesh.SetText(
            currentProgress switch
            {
                0 => string.Empty,
                1 => "ROYAL",
                2 => "EPIC",
                3 => "LEGENDARY",
                _ => string.Empty
            });

    }
    private void SetTargetProgressRectAmount(int currentProgress)
    {
        targetRect = currentProgress switch
        {
            0 => 0,
            1 => 123,
            2 => 245,
            3 => 375,
            _ => 0
        };

        if (currentProgress == 0) canvas.gameObject.SetActive(false);
        else canvas.gameObject.SetActive(true);
    }
}
