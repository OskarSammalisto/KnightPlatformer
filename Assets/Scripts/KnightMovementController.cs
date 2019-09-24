using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovementController : MonoBehaviour {
    public CharacterController2D Controller2D;
    public Joystick joystick;
    public Animator animator;
    
    //Animator Condition Strings
    private String speed = "Speed";
    private String isCrouching = "IsCrouching";

    private float joystickOffset = 0.2f;
    private float joystickJumpCrouchOffset = 0.5f;
    private float horizontalMove = 0;
    public float runSpeed = 40f;

    private bool jump = false;
    private bool crouch = false;
   
    private void Update() {
        
        

        if (joystick.Horizontal >= joystickOffset) {
            horizontalMove = runSpeed;
            animator.SetFloat(speed, Mathf.Abs(horizontalMove));
        }
        else if (joystick.Horizontal <= -joystickOffset) {
            horizontalMove = -runSpeed;
            animator.SetFloat(speed, Mathf.Abs(horizontalMove));
        }
        else {
            horizontalMove = 0;
            animator.SetFloat(speed, Mathf.Abs(horizontalMove));
        }

        float jumpCrouchCheck = joystick.Vertical;

        if (jumpCrouchCheck >= joystickJumpCrouchOffset) {
            jump = true;
        }
        

        if (jumpCrouchCheck <= -joystickJumpCrouchOffset) {
            crouch = true;
            animator.SetBool(isCrouching, crouch);
            //animator.SetFloat(speed, Mathf.Abs(horizontalMove));
        }
        else {
            crouch = false;
            animator.SetBool(isCrouching, crouch);
        }

    }

    public void OnLanding() {
        
    }

    public void OnCrouching(bool crouching) {
        animator.SetBool(isCrouching, crouching);
        animator.SetFloat(speed, Mathf.Abs(horizontalMove));
    }


    private void FixedUpdate() {
        Controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
}
