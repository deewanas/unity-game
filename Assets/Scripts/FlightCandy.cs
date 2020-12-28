using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightCandy : MonoBehaviour
{
    public static float flightSpeed;// = 3f;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -12f)
        {
             Destroy(gameObject);
        }
           
        transform.position -= new Vector3 (flightSpeed * Time.deltaTime, 0, 0);
    }
}


