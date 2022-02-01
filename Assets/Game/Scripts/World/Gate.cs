using UnityEngine;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private bool isGoodGate;
    

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("isGoodGate = " + isGoodGate);
        //}

        PlayerController player = other.attachedRigidbody.GetComponent<PlayerController>();
        if (player != null)
        {
            //Debug.Log("isGoodGate = " + isGoodGate);
            player.ChangeOutfit(isGoodGate);
            SoundManager.Instance.PlaySound(audioClip);
        }

    }






}
