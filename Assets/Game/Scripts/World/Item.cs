using UnityEngine;
using NaughtyAttributes;

public class Item : MonoBehaviour
{
    private enum ItemTypes
    {
        Princess, Witch, Neutral
    }
    [SerializeField] private ItemTypes itemType;
    [SerializeField] private AudioClip audioClip;

    private bool? isPrincessItem;
    //[SerializeField] private bool isGoodItem;
    private void Awake() => isPrincessItem = itemType switch { ItemTypes.Princess => true, ItemTypes.Witch => false, ItemTypes.Neutral => null, _ => null };

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
