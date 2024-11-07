using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer Spriterenderer;
    public float Speed;
    public ParticleSystem FeetParticles;
    public float MovingCutoff = 0.1f;
    public Animator GirlAnimator;

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

        //If moving
        if (Rigidbody2D.velocity.magnitude > MovingCutoff)
        {
            if (!FeetParticles.isPlaying)
            {
                FeetParticles.Play();
            }

            GirlAnimator.SetBool("GirlIsRunning", true);
        }
        else
        {
            if (FeetParticles.isPlaying)
            {
                FeetParticles.Stop();
            }

            GirlAnimator.SetBool("GirlIsRunning", false);
        }

        //left right check
        if (HorizontalMove > 0.5)
        {
            Spriterenderer.flipX = false;
        }
        if (HorizontalMove < -0.5)
        {
            Spriterenderer.flipX = true;
        }
    }
}
