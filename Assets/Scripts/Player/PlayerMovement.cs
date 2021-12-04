using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float Speed = 1.0f;
    [SerializeField]
    CharacterController Controller;

    //Movement Character Directions
    Vector3 Forward, Right;

    void Start()
    {
        Controller = GetComponent<CharacterController>();

        Forward = Camera.main.transform.forward;
        Forward.y = 0;
        Forward = Vector3.Normalize(Forward);
        Right = Quaternion.Euler(new Vector3(0, 90, 0)) * Forward;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 upMovement = Forward * Input.GetAxis("Vertical");
        Vector3 rightMovement = Right * Input.GetAxis("Horizontal");

        Vector3 Movement = Vector3.Normalize(rightMovement + upMovement);
        Controller.Move(Movement * Time.deltaTime * Speed);

        if (Movement != Vector3.zero)
        {
            gameObject.transform.forward = Movement;
        }

        
    }
}
