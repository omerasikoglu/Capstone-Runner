using UnityEngine;
using Unity.Collections;
using NaughtyAttributes;
using System.Collections;

public class ProgressBarUI : MonoBehaviour
{
    public static ProgressBarUI Instance { get; private set; }

    [SerializeField] private float lerpSpeed = 2f; //yeþil bar dolma hareket hýzý

    [SerializeField]private Transform barTransform; //MAX 375
    private float currentProgress = 0f; //yapýlan görev
    private float maxProgress = 3f; //toplam görev


    private void Awake()
    {
        Instance = this;

        ResetBar();
    }

    private void Start()
    {
        barTransform.localScale = new Vector3(0f, 1f, 1f);
    }
    private void Update()
    {
        CheckGreenBar();
    }

    private void CheckGreenBar()
    {
        //yeþil barýn görev tamamlandýkça yavaþça artmasý
        barTransform.localScale = new Vector3(
            Mathf.Lerp(barTransform.localScale.x, UpdateProgressAmountNormalized(), lerpSpeed * Time.deltaTime), 1f, 1f);
    }

    [Button]
    public void OneTaskDone()
    {
        currentProgress += 1;

        UpdateProgressAmountNormalized();
    }
    public void ResetBar()
    {
        currentProgress = 0;

        UpdateProgressAmountNormalized();
    }
    private float UpdateProgressAmountNormalized()
    {
        return (currentProgress / maxProgress);
    }
}
