using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(CapsuleCollider))]
public class FinishArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            if (PlayerPrefs.GetInt(StringData.PREF_MONEY) >= 1) GameManager.Instance.ChangeState(GameState.Punch);
            else GameManager.Instance.ChangeState(GameState.Fail);

            Destroy(gameObject);
        }
    }
}
