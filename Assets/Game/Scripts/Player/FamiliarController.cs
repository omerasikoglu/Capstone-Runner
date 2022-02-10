using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;

public class FamiliarController : MonoBehaviour
{
    [SerializeField, BoxGroup("[Prefab]")] private Transform butterfly, bat;

    [SerializeField, BoxGroup("[FamiliarSettings]")] private float maxY = 1f;
    [SerializeField, BoxGroup("[FamiliarSettings]")] private float selfRotateDegree = 25f;

    private Dictionary<int, GameObject> familiarDic;
    private List<Transform> familiarList;

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        OrbiteFamiliars();
    }

    private void Init()
    {
        familiarDic = new Dictionary<int, GameObject>();
        familiarDic.Clear();
        familiarList = new List<Transform>();
        familiarList.Clear();
    }

    private void OrbiteFamiliars()
    {
        //familiar'larýn eliptik yörüngede dönmesi
        transform.DORotate(new Vector3(transform.rotation.x, 360, transform.rotation.z),
            10f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            ;
    }

    [Button]
    public void AddNewButterfly()
    {
        Transform clone = Instantiate(butterfly, transform.localPosition, Quaternion.identity, transform);
        familiarList.Add(clone);
        familiarDic.Add(familiarList.Count, clone.gameObject);
        UpdateFamiliarPositions();
    } [Button]
    public void AddNewBat()
    {
        Transform clone = Instantiate(bat, transform.localPosition, Quaternion.identity, transform);
        familiarList.Add(clone);
        familiarDic.Add(familiarList.Count, clone.gameObject);
        UpdateFamiliarPositions();
    }
    [Button]
    public void RemoveOldFamiliar()
    {
        if (familiarList.Count > 0)
        {
            Destroy(familiarDic[familiarList.Count]);
            familiarDic.Remove(familiarList.Count);
            familiarList.RemoveAt(familiarList.Count - 1);

            UpdateFamiliarPositions();
        }
        else
        {
            Debug.Log("familiar sayýsý zaten 0 ");
            return;
        }
    }
    private void UpdateFamiliarPositions()
    {
        for (int i = 0; i < familiarList.Count; i++)
        {
            float angle = i * (360f / familiarList.Count);                  // 0 - 180
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.right;     // 0,180,0

            float distanceFromCenter = UnityEngine.Random.Range(1f, 2f);
            Vector3 position = transform.localPosition + direction * distanceFromCenter;

            familiarList[i].DOLocalMove(position, 1f);

        }
        SwingFamiliars();
    }
    private void SwingFamiliars()
    {
        foreach (Transform familiar in familiarList)
        {
            //familiar yukarý aþaðý hareketi
            float familiarCycleDuration = UnityEngine.Random.Range(1.5f, 2.5f);
            familiar.transform.DOLocalMoveY(maxY, familiarCycleDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo)
                    ;
            //familiar'larýn kendi etrafýnda dönmesi
            familiar.transform.DORotate(
                new Vector3(familiar.transform.rotation.x, selfRotateDegree,
                familiar.transform.rotation.z), familiarCycleDuration * 0.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                ;
        }
    }

}
