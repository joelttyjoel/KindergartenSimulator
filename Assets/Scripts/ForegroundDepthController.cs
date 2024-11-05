using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundDepthController : MonoBehaviour
{
    public GameObject Player;
    public float AboveValueSetThisInfront;

    private float ZInfront = -1;
    private float Zbehind = 1;

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.y > AboveValueSetThisInfront)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ZInfront);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Zbehind);
        }
    }
}
