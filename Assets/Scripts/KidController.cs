using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class KidController : MonoBehaviour
{
    public GameObject Player;
    public float Zoffset = 5;
    public float ZInfront = -1;
    public float Zbehind = 1;
    public Rigidbody2D ThisRigidbody2D;
    public SpriteRenderer ThisSpriteRenderer;
    public float Speed;
    public bool BeWandering;
    public float MinMoveTime;
    public float MaxMoveTime;
    public float MinStopTime;
    public float MaxStopTime;
    public Vector3 SideCheckRay1;
    public Vector3 SideCheckRay2;
    public Vector3 SideCheckRay3;
    public Vector3 SideCheckRay4;

    private RaycastHit2D SidecheckHit;
    private Vector2 CollisionCheckVector; //Points towards detected colissions

    private Vector2 RandomMovementDiretion;
    private float RandomMoveTime;
    private float RandomStopTime;
    private bool RandomMoveStopToggle; //True = Wandering, False = Stopped
    private float CurrentThingTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player back n front check
        if (Player.transform.position.y > transform.position.y + Zoffset)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ZInfront);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Zbehind);
        }
    }

    void FixedUpdate()
    {
        //Collision cheecks, always running
        Debug.DrawLine(transform.position, transform.position + SideCheckRay1, UnityEngine.Color.red);
        Debug.DrawLine(transform.position, transform.position + SideCheckRay2, UnityEngine.Color.red);
        Debug.DrawLine(transform.position, transform.position + SideCheckRay3, UnityEngine.Color.red);
        Debug.DrawLine(transform.position, transform.position + SideCheckRay4, UnityEngine.Color.red);

        CollisionCheckVector = new Vector3(0, 0, 0);

        //Colissions check
        SidecheckHit = Physics2D.Raycast(transform.position, SideCheckRay1, SideCheckRay1.magnitude);
        if(SidecheckHit)
        {
            CollisionCheckVector = CollisionCheckVector - (new Vector2(transform.position.x, transform.position.y) - SidecheckHit.point);
        }
        SidecheckHit = Physics2D.Raycast(transform.position, SideCheckRay2, SideCheckRay2.magnitude);
        if (SidecheckHit)
        {
            CollisionCheckVector = CollisionCheckVector - (new Vector2(transform.position.x, transform.position.y) - SidecheckHit.point);
        }
        SidecheckHit = Physics2D.Raycast(transform.position, SideCheckRay3, SideCheckRay3.magnitude);
        if (SidecheckHit)
        {
            CollisionCheckVector = CollisionCheckVector - (new Vector2(transform.position.x, transform.position.y) - SidecheckHit.point);
        }
        SidecheckHit = Physics2D.Raycast(transform.position, SideCheckRay4, SideCheckRay4.magnitude);
        if (SidecheckHit)
        {
            CollisionCheckVector = CollisionCheckVector - (new Vector2(transform.position.x, transform.position.y) - SidecheckHit.point);
        }

        Debug.Log(CollisionCheckVector);

        //Avoid obstacles by applying force but inverse to checkmagnitude length
        CollisionCheckVector = FlipVector(CollisionCheckVector);
        CollisionCheckVector.Normalize();
        ThisRigidbody2D.AddForce(CollisionCheckVector * Speed);

        Debug.Log(CollisionCheckVector);

        //If should be wandering
        if (BeWandering)
        {
            //Moving
            if (RandomMoveStopToggle)
            {
                CurrentThingTimer = CurrentThingTimer + Time.fixedDeltaTime;

                ThisRigidbody2D.AddForce(RandomMovementDiretion * Speed);

                if (CurrentThingTimer > RandomMoveTime || CollisionCheckVector.magnitude > 0)
                {
                    //Set new stop time and go to stop
                    RandomMoveTime = Random.Range(MinStopTime, MaxStopTime);
                    CurrentThingTimer = 0;

                    RandomMoveStopToggle = false;
                }
            }
            //Stopped
            else
            {
                CurrentThingTimer = CurrentThingTimer + Time.fixedDeltaTime;

                if (CurrentThingTimer > RandomMoveTime)
                {
                    //Set new move time and go to move
                    RandomMoveTime = Random.Range(MinMoveTime, MaxMoveTime);
                    CurrentThingTimer = 0;
                    //Set new direction
                    RandomMovementDiretion = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                    RandomMoveStopToggle = true;
                }
            }
        }

        //left right check
        if (Input.GetAxisRaw("Horizontal") > 0.5)
        {
            ThisSpriteRenderer.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.5)
        {
            ThisSpriteRenderer.flipX = true;
        }
    }

    public static Vector2 FlipVector(Vector2 v)
    {
        return new Vector2(
            v.x * -1,
            v.y * -1
        );
    }
}
