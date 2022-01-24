using UnityEngine;
using Unity.Collections;
using NaughtyAttributes;
using System.Collections;

public class ProgressBarUI : MonoBehaviour
{
    public static ProgressBarUI Instance { get; private set; }

    [SerializeField] private Transform heartOn, heartOff;


    [SerializeField] private float lerpSpeed = 2f; //yeþil bar dolma hareket hýzý

    private Transform barTransform;
    private float currentProgress = 0f; //yapýlan görev
    private float maxProgress = 3f; //toplam görev


    private void Awake()
    {
        Instance = this;

        barTransform = transform.Find(StringData.BAR);
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
        barTransform.localScale = new Vector3(Mathf.Lerp(barTransform.localScale.x, UpdateProgressAmountNormalized(), lerpSpeed * Time.deltaTime), 1f, 1f);
        if (currentProgress >= maxProgress)
        {
            heartOn.gameObject.SetActive(true);
            heartOff.gameObject.SetActive(false);
        }
    }

    [Button]
    public void OneTaskDone()
    {
        currentProgress += 1;
        if (currentProgress == maxProgress)
        {
            //tüm görevler yapýldýysa
        }

        UpdateProgressAmountNormalized();
    }
    public int GetScore()
    {
        return (int)currentProgress;
    }
    public void ResetBar()
    {

        heartOn.gameObject.SetActive(false);
        heartOff.gameObject.SetActive(true);
        currentProgress = 0;

        UpdateProgressAmountNormalized();
    }
    private float UpdateProgressAmountNormalized()
    {
        return (currentProgress / maxProgress);
    }
}
