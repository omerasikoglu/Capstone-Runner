using UnityEngine;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private bool isPrincessGate;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player")){ }

        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.ChangeOutfit(isPrincessGate);
            SoundManager.Instance.PlaySound(audioClip);
            //TODO: KAFANIN UZERINDEK� BAR ARTCAK YAZI CIKCAK
            //TODO: ayn� anda 1 kap�dan ge�me
        }

    }






}
