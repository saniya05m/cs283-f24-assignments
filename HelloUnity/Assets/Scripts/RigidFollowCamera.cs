using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    // Public parameters to set in the Inspector
    public Transform target; // The player character to follow
    public float horizontalDistance = 5.0f; // Horizontal follow distance
    public float verticalDistance = 3.0f; // Vertical follow distance

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for RigidFollowCamera.");
            return;
        }

        // Get the target's position, up vector, and forward vector
        Vector3 tPos = target.position;
        Vector3 tUp = target.up;
        Vector3 tForward = target.forward;

        // Calculate the camera's new position (eye)
        Vector3 eye = tPos - tForward * horizontalDistance + tUp * verticalDistance;

        // Calculate the direction the camera should face
        Vector3 cameraForward = tPos - eye;

        // Set the camera's position and rotation
        transform.position = eye;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}

