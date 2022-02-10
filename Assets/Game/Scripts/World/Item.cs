using UnityEngine;
using NaughtyAttributes;

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

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.ChangeItemPoint(isPrincessItem);
            SoundManager.Instance.PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
