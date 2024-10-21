using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;          // The target to track
    public Transform lookJoint;       // The joint that should point towards the target (e.g., head, eyes, antenna)

    // Update is called once per frame
    void Update()
    {
        if (target != null && lookJoint != null)
        {
            // Vector from the lookJoint to the target (r)
            Vector3 e = target.position - lookJoint.position;
            // The forward direction of the joint (e)
            Vector3 r = lookJoint.forward;

            // Compute the cross product (r x e) to get the rotation axis
            Vector3 crossProduct = Vector3.Cross(r, e);

            // Compute the magnitude of the cross product
            float crossMagnitude = crossProduct.magnitude;

            // Compute the dot product (r . e)
            float dotProduct = Vector3.Dot(r, e);



            // If the cross product magnitude is not zero, compute the axis of rotation
            if (crossMagnitude > 0.0001f)  // Avoid extremely small values
            {
                // Compute the angle of rotation using the provided formula
                float angle = Mathf.Atan2(crossMagnitude, Mathf.Abs(Vector3.Dot(r, r) + Vector3.Dot(r, e)));
                Vector3 axis = crossProduct.normalized;
                lookJoint.Rotate(axis, angle);

            }

            // Visualize the line between the joint and the target
            Debug.DrawLine(lookJoint.position, target.position, Color.red);
        }
    }
}


