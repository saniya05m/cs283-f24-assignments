using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Public parameters for linear and turning speed
    public float linearSpeed = 5.0f;
    public float turningSpeed = 100.0f;

    // Update is called once per frame
    void Update()
    {
        // Move forward or backward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * linearSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * linearSpeed * Time.deltaTime);
        }

        // Turn left or right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
        }
    }
}
