using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float Speed;

    private float HorizontalMove;
    private float VerticalMove;
    private Vector2 MoveVector;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");

        MoveVector = new Vector2(HorizontalMove, VerticalMove);

        Rigidbody2D.AddForce(MoveVector * Speed);
    }
}
