using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(BoxCollider))]
public class Gate : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private bool isGoodGate;

    private void Awake() => GetComponent<BoxCollider>().isTrigger = true;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player")){ }

        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.Log("isGoodGate = " + isGoodGate);
            player.ChangeOutfit(isGoodGate);
            SoundManager.Instance.PlaySound(audioClip);
        }

    }






}
