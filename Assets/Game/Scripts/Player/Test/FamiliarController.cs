using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;

public class FamiliarController : MonoBehaviour
{
    [SerializeField] private List<Transform> familiarList;

    private float maxY = 0.5f;

    private void Start()
    {
        SwingFamiliars();
    }

    private void SwingFamiliars()
    {
        foreach (Transform familiar in familiarList)
        {
            //familiar yukar� a�a�� hareketi
            float familiarCycleDuration = UnityEngine.Random.Range(2f, 5f);
            familiar.transform.DOLocalMoveY(maxY, familiarCycleDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo)
                    ;
            //kendi etraf�nda d�nmesi
            familiar.transform.DORotate(
                new Vector3(familiar.transform.rotation.x, 25f, familiar.transform.rotation.z),
                familiarCycleDuration * 0.5f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                ;
            //eliptik y�r�ngede d�nmesi
            transform.DORotate(new Vector3(transform.rotation.x, 360, transform.rotation.z),
                10f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Restart)
                ;
        }
    }
}
