using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSettings playerSettings;

    private Rigidbody rb => GetComponent<Rigidbody>();

    private bool canMove = false;
    
    private void Start()
    {

    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove && InputManager.IsClickDownAnything)
        {
            //first move
            canMove = !canMove;
            UIManager.Instance.SetDeactiveTapToStartUI();
        }
        if (canMove && rb.velocity.x < playerSettings.MaxSpeed)
        {
            //3f'e kadar hızlansın sonra hızı sabit kalsın
            rb.velocity += Vector3.forward * playerSettings.MovementSpeed * Time.deltaTime;
        }
    }

}