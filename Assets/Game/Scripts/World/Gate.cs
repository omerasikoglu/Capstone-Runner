using UnityEngine;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private bool isPrincessGate;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.GateIsPassed(isPrincessGate);
            SoundManager.Instance.PlaySound(audioClip);

            
            //TODO: KAFANIN UZERINDEKÝ BAR ARTCAK YAZI CIKCAK


        }

    }






}
