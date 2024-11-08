using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 0.125f;
    public Vector3 Offset;

    private Vector3 DesiredPosition;
    private Vector3 SmoothedPosition;

    void FixedUpdate()
    {
        DesiredPosition = Target.transform.position + Offset;
        SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, Speed);
        transform.position = new Vector3(SmoothedPosition.x, SmoothedPosition.y, transform.position.z);
    }
}
