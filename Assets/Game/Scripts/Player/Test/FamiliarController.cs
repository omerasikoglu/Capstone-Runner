using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;

public class FamiliarController : MonoBehaviour
{
    [SerializeField] private List<Transform> familiarList;

    [SerializeField] private int familiarCount;
    [SerializeField] private float distanceFromCenter;
    [SerializeField] private float rotationSpeed;
    private float maxY = 0.5f, minY = -0.5f;

    private void Start()
    {
        SwingFamiliars();
    }

    private void SwingFamiliars()
    {
        foreach (Transform familiar in familiarList)
        {
            //familiar yukarý aþaðý hareketi
            float familiarMoveSpeed = UnityEngine.Random.Range(2f, 5f);
            familiar.transform.DOLocalMoveY(maxY, familiarMoveSpeed)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo)
                    ;
            //eliptik yörüngede dönmesi
            familiar.transform.DORotate(new Vector3(
                familiar.transform.rotation.x, 25f, familiar.transform.rotation.z), 3)
                .SetLoops(-1, LoopType.Yoyo)
                ;


        }

    }

    private void Update()
    {

    }



}
