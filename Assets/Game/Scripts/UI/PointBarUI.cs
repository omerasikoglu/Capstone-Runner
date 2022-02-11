using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class PointBarUI : MonoBehaviour
{
    public static PointBarUI Instance { get; private set; }

    [SerializeField] private RectTransform barTransform; //MAX 350
    [SerializeField] private float lerpSpeed = 4f; //yeþil bar dolma hareket hýzý

    [SerializeField] private int maxItemCount = 40; //toplam altýn
    private int currentPoint = 0; //alýnan altýn

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
        //yeþil barýn görev tamamlandýkça yavaþça artmasý
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
