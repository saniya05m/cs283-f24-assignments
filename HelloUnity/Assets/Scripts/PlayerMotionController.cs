using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    // Public parameters for linear and turning speed
    public float linearSpeed = 5.0f;
    public float turningSpeed = 100.0f;
    private CharacterController controller;
    private Animator animator;

    // Initialization
    void Start()
    {   controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    } 

    // Update is called once per frame
    void Update()
    {   bool isMoving = false;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Move forward or backward
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector3.forward * linearSpeed * Time.deltaTime);

            controller.Move(linearSpeed * Time.deltaTime * -transform.forward);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(-Vector3.forward * linearSpeed * Time.deltaTime);
            controller.Move(linearSpeed * Time.deltaTime * transform.forward);
            isMoving = true;
        }

        // Turn left or right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.I)){
            StartCoroutine(Interact());
        }

        if(Input.GetKey(KeyCode.K)){
            StartCoroutine(Sword());
        }
        
        IEnumerator Interact(){
        animator.SetBool("isInteract", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isInteract", false);
    }

    IEnumerator Sword(){
        animator.SetBool("isSword", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isSword", false);
    }
     
        // implemented gravity sort of, so that the character doesn't end up floating
        controller.Move(Vector3.down * 9.8f * Time.deltaTime);
        animator.SetBool("isMoving", isMoving);
    }
}
