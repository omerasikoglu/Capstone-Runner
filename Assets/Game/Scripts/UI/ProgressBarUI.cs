using UnityEngine;
using Unity.Collections;
using NaughtyAttributes;
using System.Collections;

public class ProgressBarUI : MonoBehaviour
{
    public static ProgressBarUI Instance { get; private set; }

    [SerializeField] private float lerpSpeed = 2f; //ye�il bar dolma hareket h�z�

    [SerializeField]private Transform barTransform; //MAX 375
    private float currentProgress = 0f; //yap�lan g�rev
    private float maxProgress = 3f; //toplam g�rev


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
        //ye�il bar�n g�rev tamamland�k�a yava��a artmas�
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
