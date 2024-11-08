using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f; 
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        // don't compare float numbers to 0
        //if(period == 0f) this code underneath is used instead of
        // this because Mathf.Epsilon is the smallest number in unity
        if(period <= Mathf.Epsilon)
        {
            return;
        }
        //continuelly growing over time
        float cycles = Time.time / period; 
        // const value so we can have a sin value cycle
        const float tau = Mathf.PI * 2;
        // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);
        // recalculated going from 0 to 1
        movementFactor = (rawSinWave + 1f) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
