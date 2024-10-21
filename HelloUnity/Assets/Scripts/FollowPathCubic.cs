using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathCubic : MonoBehaviour
{
    public List<Transform> POIs; 
    public float duration = 3.0f;       
    public bool DeCasteljau = false;    
    private int currentPointIndex = 1; 

    private bool isMoving = false;      // To track if the character is currently moving

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving){
            StartCoroutine(FollowPath());
        }
    }

    // Cubic Bezier curve coroutine
    IEnumerator FollowPath()
    {   isMoving = true;  // Set the flag to true to indicate that the movement has started
        while (currentPointIndex < POIs.Count - 1)  // Iterate between segments
        {
            Transform b0 = POIs[currentPointIndex - 1];
            Transform b3 = POIs[currentPointIndex];
            Transform nextPoint = POIs[(currentPointIndex + 1) % POIs.Count];

            Vector3 b1, b2, prevpos;

            prevpos = POIs[0].position;

            if (currentPointIndex == 1)  // If it's the first segment
            {
                b1 = b0.position + (1f / 6f) * (b3.position - b0.position);
            }
            else
            {
                Transform prevPoint = POIs[currentPointIndex - 2];
                b1 = b0.position + (1f / 6f) * (b3.position - prevPoint.position);
            }

            if (currentPointIndex == POIs.Count - 2)  // If it's the last segment
            {
                b2 = b3.position - (1f / 6f) * (b3.position - b0.position);
            }
            else
            {
                b2 = b3.position - (1f / 6f) * (nextPoint.position - b0.position);
            }

            // Move along the curve
            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float t = timer / duration;
                Vector3 position;

                if (DeCasteljau)
                {
                    position = ComputeDeCasteljau(b0.position, b1, b2, b3.position, t);
                }
                else
                {
                    position = ComputeBezierCubic(b0.position, b1, b2, b3.position, t);
                }

                transform.position = position;
                // Set rotation to match the forward direction


                Vector3 forwardDirection = (position - prevpos).normalized;

                prevpos = position;

                if (forwardDirection != Vector3.zero)  // To prevent errors when both points are the same
                {
                    Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
                }


                yield return null;  // Wait until the next frame
            }

            currentPointIndex++;
        }
        isMoving = false; //Movement done
    }

    // Polynomial formula for cubic Bezier curve
    Vector3 ComputeBezierCubic(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, float t)
    {
        float u = 1 - t;
        return u * u * u * b0 + 3 * u * u * t * b1 + 3 * u * t * t * b2 + t * t * t * b3;
    }

    // De Casteljau's algorithm for cubic Bezier curve
    Vector3 ComputeDeCasteljau(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, float t)
    {
        Vector3 ab = Vector3.Lerp(b0, b1, t);
        Vector3 bc = Vector3.Lerp(b1, b2, t);
        Vector3 cd = Vector3.Lerp(b2, b3, t);
        Vector3 abc = Vector3.Lerp(ab, bc, t);
        Vector3 bcd = Vector3.Lerp(bc, cd, t);
        return Vector3.Lerp(abc, bcd, t);
    }
}

