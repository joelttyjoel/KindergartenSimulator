using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kitchen : MonoBehaviour
{
    public float TimeFood = 3.0f;
    Collider InStoveArea;

    // Start is called before the first frame update
    void Start()
    {
        InStoveArea = GetComponent<Collider>();
        InStoveArea.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("E") && InStoveArea.isTrigger)
        {
            TimeFood -= Time.deltaTime;
            if (TimeFood < 0)
            {
                
            }
            else
            {
                TimeFood = 3.0f;
            }
        };
    }
}
