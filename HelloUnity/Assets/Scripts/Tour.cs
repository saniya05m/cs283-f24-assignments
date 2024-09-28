using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tour : MonoBehaviour
{   public GameObject Camera;
    public Transform[] pointsOfInterest; // Array to store the list of POIs
    public float speed = 5f;             // Speed parameter to control the camera's movement
    private int currentPOIIndex = 0;     // Index of the current POI
    private bool isTransitioning = false; // Boolean to track if we are transitioning between POIs
    private float transitionProgress = 0f; // Progress of the current transition

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float transitionDuration; // Time needed for transition between POIs
    private FlyCamera flyCamera;

    void Start()
    {
        // Find the FlyCamera script attached to the same GameObject
        flyCamera = Camera.GetComponent<FlyCamera>();
    }
    void Update()
    {
        // Check for 'N' key press to move to the next POI
        if (Input.GetKeyDown(KeyCode.N) && !isTransitioning)
        {
            // Prepare for transition
            StartTransitionToNextPOI();
            if (flyCamera != null)
            {
                flyCamera.SetTourActive(true); // Disable FlyCamera controls
            }
        }

        // Handle the camera movement during the transition
        if (isTransitioning)
        {
            MoveCameraToPOI();
        }
    }

    void StartTransitionToNextPOI()
    {
        // Store the starting position and rotation of the camera
        startPosition = Camera.transform.position;
        startRotation = Camera.transform.rotation;

        // Compute the duration based on the speed and distance to the next POI
        float distance = Vector3.Distance(Camera.transform.position, pointsOfInterest[currentPOIIndex].position);
        transitionDuration = distance / speed;

        // Reset progress
        transitionProgress = 0f;

        // Set flag to start transition
        isTransitioning = true;
    }

    void MoveCameraToPOI()
    {
        // Increment the progress based on time elapsed
        transitionProgress += Time.deltaTime / transitionDuration;

        // Interpolate position and rotation using Lerp and Slerp
        Camera.transform.position = Vector3.Lerp(startPosition, pointsOfInterest[currentPOIIndex].position, transitionProgress);
        Camera.transform.rotation = Quaternion.Slerp(startRotation, pointsOfInterest[currentPOIIndex].rotation, transitionProgress);

        // Check if transition is complete
        if (transitionProgress >= 1f)
        {
            // Snap to the exact final position and rotation (in case Lerp/Slerp didn't perfectly align)
            Camera.transform.position = pointsOfInterest[currentPOIIndex].position;
            Camera.transform.rotation = pointsOfInterest[currentPOIIndex].rotation;

            // Move to the next POI index
            currentPOIIndex = (currentPOIIndex + 1) % pointsOfInterest.Length;

            // End transition
            isTransitioning = false;
            if (flyCamera != null)
            {
                flyCamera.SetTourActive(false); // Re-enable FlyCamera controls
            }
        }
        
    }
}
