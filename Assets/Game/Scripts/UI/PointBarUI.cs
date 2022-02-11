using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class PointBarUI : MonoBehaviour
{
    public static PointBarUI Instance { get; private set; }

    [SerializeField] private RectTransform barTransform; //MAX 350
    [SerializeField] private float lerpSpeed = 4f; //ye�il bar dolma hareket h�z�

    [SerializeField] private int maxItemCount = 40; //toplam alt�n
    private int currentPoint = 0; //al�nan alt�n

    private float maxRect = 350;
    private float targetRect;

    private void Awake()
    {
        Instance ??= this;
    }
    private void Update()
    {
        CheckGreenBar();
    }
    private void CheckGreenBar()
    {
        //ye�il bar�n g�rev tamamland�k�a yava��a artmas�
        barTransform.sizeDelta = new Vector2(
            Mathf.Lerp(barTransform.sizeDelta.x, GetTargetProgressAmount(), lerpSpeed * Time.deltaTime), 44f);
    }
    [Button]
    public void ResetBar()
    {
        PlayerPrefs.SetInt(StringData.PREF_POINT, 0);

        SetTargetItemRectAmount();
    }
    [Button]
    public void SetTargetItemRectAmount()
    {
        targetRect = maxRect * Mathf.InverseLerp(0, maxItemCount, PlayerPrefs.GetInt(StringData.PREF_POINT));
    }

    private float GetTargetProgressAmount()
    {
        return targetRect;
    }

}
