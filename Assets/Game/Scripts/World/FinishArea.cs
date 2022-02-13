using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(CapsuleCollider))]
public class FinishArea : MonoBehaviour
{
    [SerializeField] private Transform witchDummy, princessDummy;

    private void Start()
    {
        ResetToDefault();
    }

    private void ResetToDefault()
    {
        witchDummy.gameObject.SetActive(false);
        princessDummy.gameObject.SetActive(false);
        witchDummy.transform.localPosition = new Vector3(0f,0f,0.1f);
        princessDummy.transform.localPosition = new Vector3(0f, 0f, 0.1f);
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
            StartCoroutine(UtilsClass.WaitCertainAmountOfTime(() => { ResetToDefault(); }, 10f));
        }
    }
}
