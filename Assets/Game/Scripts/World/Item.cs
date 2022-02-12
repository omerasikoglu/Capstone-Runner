using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;

public class Item : MonoBehaviour
{
    private enum ItemType
    {
        Princess, Witch, Neutral
    }
    [SerializeField] private ItemType itemType;
    [SerializeField] private AudioClip audioClip;

    private bool? isPrincessItem;
    //[SerializeField] private bool isGoodItem;
    private void Awake() => isPrincessItem = itemType switch
    { ItemType.Princess => true, ItemType.Witch => false, ItemType.Neutral => null, _ => null };

    private void Start()
    {
        IdleAnimation();
    }

    private void IdleAnimation()
    {
       
            float itemCycleDuration = UnityEngine.Random.Range(1.5f, 2.5f);
            transform.DOLocalMoveY(0.2f, itemCycleDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo)
                    ;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.SomeCollectibleHasTaken(isPrincessItem);

            if (audioClip != null)
            {
                SoundManager.Instance.PlaySound(audioClip);
            }
            Destroy(gameObject);
        }
    }
}
