using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLinkController : MonoBehaviour
{
    public Transform target;           // The target the IK system will track
    public Transform endEffector;      // The end effector (like the hand)
    
    private Transform middleJoint;     // The middle joint (elbow)
    private Transform baseJoint;       // The base joint (shoulder)

    public float upperArmLength = 1.0f;  // Distance from baseJoint to middleJoint (like the upper arm)
    public float forearmLength = 1.0f;   // Distance from middleJoint to endEffector (like the forearm)

    void Start()
    {
        // Assume that the middle joint is the parent of the end effector
        middleJoint = endEffector.parent;

        // Assume that the base joint (shoulder) is the grandparent of the end effector
        baseJoint = middleJoint.parent;
    }

    void Update()
    {
        // Calculate the distance between baseJoint (shoulder) and the target
        Vector3 baseToTarget = target.position - baseJoint.position;
        float targetDistance = baseToTarget.magnitude;

        // Check if the target is within reach (upper arm + forearm)
        float armLength = upperArmLength + forearmLength;
        if (targetDistance > armLength)
        {
            Debug.Log("Target is out of reach");
            targetDistance = armLength;  // Limit to the maximum reach of the arm
        }

        // Calculate the position of the middle joint (elbow) using the law of cosines
        float a = upperArmLength;
        float b = forearmLength;
        float c = targetDistance;

        float cosAngleElbow = (a * a + b * b - c * c) / (2 * a * b);
        float elbowAngle = Mathf.Acos(cosAngleElbow) * Mathf.Rad2Deg;

        // Rotate the middle joint (elbow)
        Vector3 elbowDir = baseToTarget.normalized;
        middleJoint.rotation = Quaternion.LookRotation(elbowDir);

        // Calculate the bend direction (visualize with Debug.DrawLine)
        Vector3 bendAxis = Vector3.Cross(baseJoint.up, elbowDir);
        Debug.DrawLine(middleJoint.position, middleJoint.position + bendAxis, Color.green);

        // Rotate the base joint (shoulder) to aim toward the target
        Quaternion shoulderRotation = Quaternion.LookRotation(baseToTarget.normalized, baseJoint.up);
        baseJoint.rotation = shoulderRotation;

        // Adjust the end effector to align with the target
        endEffector.position = target.position;

        // Visualize the reach
        Debug.DrawLine(baseJoint.position, middleJoint.position, Color.red); // Upper arm
        Debug.DrawLine(middleJoint.position, endEffector.position, Color.blue); // Forearm

        // Check if the arm length matches the target distance
        float endEffectorDistance = Vector3.Distance(baseJoint.position, endEffector.position);
        if (Mathf.Abs(endEffectorDistance - armLength) > 0.01f)
        {
            Debug.Log("Arm length does not match the target distance");
        }
    }
}
