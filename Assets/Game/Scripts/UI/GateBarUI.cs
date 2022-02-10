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
    private int currentProgress = 0; //yapýlan görev
    private int maxProgress = 3; //toplam görev

    private void Awake()
    {
        ResetBar();
    }

    private void Start()
    {
        SetTargetProgressAmount(currentProgress);
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
        currentProgress = Mathf.Clamp(currentProgress, 0, 3);

        SetTargetProgressAmount(currentProgress);
        SetWritings(currentProgress);
        StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { HideBar(); }, 4f));
    }

    public void ResetBar()
    {
        currentProgress = 0;

        SetTargetProgressAmount(currentProgress);
    }
    private float GetTargetProgressAmount()
    {
        return targetRect;
    }
    private void SetWritings(int currentProgress)
    {
        //string text = currentProgress switch
        //{
        //    0 => string.Empty,
        //    1 => "11111",
        //    2 => "22222",
        //    3 => "33333",
        //    _ => string.Empty
        //};
        //textMesh.SetText(text);

        textMesh.SetText(
            currentProgress switch
            {
                0 => string.Empty,
                1 => "11111",
                2 => "22222",
                3 => "33333",
                _ => string.Empty
            });

    }
    private void SetTargetProgressAmount(int currentProgress)
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
        //barTransform.localScale = new Vector3(0.005f, 0.01f, 0.01f);
    }
}
