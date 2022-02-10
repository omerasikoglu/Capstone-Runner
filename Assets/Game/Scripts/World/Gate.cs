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
            //TODO: KAFANIN UZERINDEKÝ BAR ARTCAK YAZI CIKCAK
            //TODO: ayný anda 1 kapýdan geçme
        }

    }






}
