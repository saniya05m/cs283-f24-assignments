using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpringFollowCamera : MonoBehaviour
{
    // Public parameters to set in the Inspector
    public Transform target; // The player character to follow
    public float horizontalDistance = 5.0f; // Desired horizontal follow distance
    public float verticalDistance = 3.0f; // Desired vertical follow distance
    public float dampConstant = 0.5f; // Damping factor to reduce oscillation
    public float springConstant = 10.0f; // Spring constant to control the strength of the spring force

    private Vector3 velocity = Vector3.zero; // Camera velocity for spring integration
    private Vector3 actualPosition; // Actual position of the camera

    // Start is called before the first frame update
    void Start()
    {
        actualPosition = transform.position; // Initialize actual position to camera's starting position
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for SpringFollowCamera.");
            return;
        }

        // Get the target's position, up vector, and forward vector
        Vector3 tPos = target.position;
        Vector3 tUp = target.up;
        Vector3 tForward = target.forward;

        // Calculate the camera's ideal position (idealEye)
        Vector3 idealEye = tPos - tForward * horizontalDistance + tUp * verticalDistance;

        // Calculate the spring force
        Vector3 displacement = actualPosition - idealEye;
        Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);

        // Integrate the camera's velocity and position
        velocity += springAccel * Time.deltaTime;
        actualPosition += velocity * Time.deltaTime;

        // Calculate the direction the camera should face
        Vector3 cameraForward = tPos - actualPosition;

        // Set the camera's position and rotation
        transform.position = actualPosition;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}

