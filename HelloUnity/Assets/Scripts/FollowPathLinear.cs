using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
    public List<Transform> POIs;  
    public float duration = 3.0f;       
    private int currentPointIndex = 0;  
    private bool isMoving = false;      // To track if the character is currently moving

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoLerp());
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {   
            StartCoroutine(DoLerp());
        }
    }

    // Coroutine to move between path points using Lerp
    IEnumerator DoLerp()
    {   currentPointIndex = 0;
        isMoving = true;  // Set the flag to true to indicate that the movement has started

        while (currentPointIndex < POIs.Count - 1)  // Move between points until the last one
        {
            Transform start = POIs[currentPointIndex];
            Transform end = POIs[currentPointIndex + 1];

            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float u = timer / duration;
                // Lerp position
                transform.position = Vector3.Lerp(start.position, end.position, u);

                Vector3 forwardDirection = (end.position - start.position).normalized;

                if (forwardDirection != Vector3.zero)  // To prevent errors when both points are the same
                {
                    Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, u);
                }
                yield return null;  // Wait until the next frame
            }

            currentPointIndex++;
        }

        isMoving = false;  // Movement is done
    }
}


