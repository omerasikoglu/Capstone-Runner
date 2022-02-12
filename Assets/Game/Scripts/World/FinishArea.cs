using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(CapsuleCollider))]
public class FinishArea : MonoBehaviour
{
    [SerializeField] private Transform witchDummy, princessDummy;

    private void Start()
    {
        witchDummy.gameObject.SetActive(false);
        princessDummy.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            //set active
            if (player.GetAreYouPrincess() == true) witchDummy.gameObject.SetActive(true);
            else princessDummy.gameObject.SetActive(true);

            //start punch
            if (PlayerPrefs.GetInt(StringData.PREF_POINT) > 10) GameManager.Instance.ChangeState(GameState.Punch);
            else GameManager.Instance.ChangeState(GameState.Fail);

            //set disable
            StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { witchDummy.gameObject.SetActive(false); princessDummy.gameObject.SetActive(false); }, 10f));
        }
    }
}
