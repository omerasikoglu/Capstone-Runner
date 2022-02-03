using UnityEngine;
using NaughtyAttributes;

public class Clickable : MonoBehaviour
{


    public Transform playerPos3D;


    private void Update()
    {
        UtilsClass.GetScreenToWorldPoint(playerPos3D.position);
    }


}
