using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RocketMovementRestrict : MonoBehaviour
{
    [SerializeField] float minX = -10f;
    [SerializeField] float maxX = 10f;
    [SerializeField] float minY = -10f;
    [SerializeField] float maxY = 10f;

    void Update()
    {
        // Get the current position of the camera
        Vector3 currentPosition = transform.position;

        // Clamp the position to the specified bounds
        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Update the camera's position
        transform.position = new Vector3(clampedX, clampedY, currentPosition.z);
    }
}

