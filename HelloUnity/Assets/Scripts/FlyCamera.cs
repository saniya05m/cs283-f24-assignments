using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    // Mouse sensitivity settings
    public float sensitivity = 100f;

    // Speed for movement
    public float moveSpeed = 10f;
    private float pitch = 0f;

    private bool isTourActive = false;
    void Start()
    {
        // Hide and lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {   if (!isTourActive)
            {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Calculate pitch (up/down) and clamp it to prevent over-rotation
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply yaw rotation (left/right) to the Y axis
        transform.Rotate(Vector3.up * mouseX, Space.World);

        // Apply pitch rotation (up/down) to the X axis
        transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, 0f);

        // Handle movement with WASD keys
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // W/S or Up/Down Arrow

        // Move the camera relative to its orientation
        transform.Translate(moveX, 0f, moveZ);
        }

        
    }
     
    // Public method to enable/disable FlyCamera behavior
    public void SetTourActive(bool isActive)
    {
        isTourActive = isActive;
    }
}
