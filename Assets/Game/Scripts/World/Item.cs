using UnityEngine;
using NaughtyAttributes;

public class Item : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private bool isGoodItem;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.attachedRigidbody.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeItemPoint(isGoodItem);
            SoundManager.Instance.PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
