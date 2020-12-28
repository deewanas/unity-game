using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class SpawnCandys : MonoBehaviour
{
    public GameObject candy;
    public GameObject marshmallow;
    float speed = 1f;
    float wait = 7f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (Spawn ());
        
    }

    IEnumerator Spawn() {
        while(true) {
            int mIndex = Random.Range(0,3);
            speed += 0.2f;

            if(mIndex == 1)
            {
                Instantiate(candy, new Vector2 ( 11.25f, Random.Range(1.5f, 4f)), Quaternion.identity);
                FlightCandy.flightSpeed = speed;
            }
            else if(mIndex == 0 || mIndex == 2)
            {
                Instantiate(marshmallow, new Vector2 ( 11.25f, Random.Range(1.5f, 4f)), Quaternion.identity);
                FlightMarshmallow.flightSpeed = speed;
            }
            if(wait > 0.8f)
            {
                wait -= 0.2f;
            }
            yield return new WaitForSeconds(wait);
        }
    }
   
}
