﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnightMovementController : MonoBehaviour {
    public CharacterController2D Controller2D;
    public Joystick joystick;
    public Animator animator;
    
    //Animator Condition Strings
    private String speed = "Speed";
    private String isCrouching = "IsCrouching";
    private String isjumping = "IsJumping";

    private float joystickOffset = 0.2f;  //Offset until player moves
    private float joystickJumpCrouchOffset = 0.8f;  //Offset until player jumps/crouches
    private bool canJump = true;          //prevents instant rejump on landing  
    private float nextJumpDelay = 1f;    //set delay between jumps
    private float horizontalMove = 0;        
    private float runSpeed = 20f;

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

        if (jumpCrouchCheck >= joystickJumpCrouchOffset && canJump) {
            jump = true;
            animator.SetBool(isjumping, jump);
            StartCoroutine(ReJumpDelay());

        }
        

        if (jumpCrouchCheck <= -joystickJumpCrouchOffset) {
            crouch = true;
            animator.SetBool(isCrouching, crouch);
            
        }
        else {
            crouch = false;
            animator.SetBool(isCrouching, crouch);
        }

    }

    public void OnLanding() {
        animator.SetBool(isjumping, false);
        
        
    }

    public void OnCrouching(bool crouching) {
        animator.SetBool(isCrouching, crouching);
        animator.SetFloat(speed, Mathf.Abs(horizontalMove));
    }


    private void FixedUpdate() {
        Controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        if (jump == true) {
           // canJump = Time.time + nextJumpDelay;
        }
        jump = false;

    }

    
    //Prevents player from jumping again instantly upon landing
    IEnumerator ReJumpDelay() {
        
        yield return new WaitForSeconds(nextJumpDelay);
        canJump = false;
        yield return  new WaitForSeconds(nextJumpDelay);
        canJump = true;
    }

   
}
