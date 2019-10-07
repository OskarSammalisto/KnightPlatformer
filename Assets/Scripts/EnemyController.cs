﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject particles;
    [HideInInspector]
    public Animator animator;

    public BoxCollider2D trigger;

    public Transform groundCheckStart;
    public Transform groundCheckEnd;
    public Transform frontGroundCheckEnd;
    public LayerMask avoidCollisionLayer;

    //animator hash
    private int stabHash = Animator.StringToHash("stab");
    private int gotHit = Animator.StringToHash("GotHit");
    
    private float health;
    private float damage = 20f;
    private int direction = 1;
    private bool canFlip = true;

    private float secondAttackDelay = 1.5f;
    private bool canTrigger = true;

    
    void Start() {

        canTrigger = true;
        health = 5f;
        particles.SetActive(false);
        animator = GetComponent<Animator>();

        HitStateBehaviour hitStateBehaviour = animator.GetBehaviour<HitStateBehaviour>();
        hitStateBehaviour.enemyController = this;

    }

   
    void Update() {
        

        if (canFlip) {
            RaycastHit2D hit = Physics2D.Linecast(groundCheckStart.position, groundCheckEnd.position, avoidCollisionLayer);
            
            if (hit.collider == null) {
                canFlip = false;
                
                direction *= -1;
                StartCoroutine(FlipDelay());
                
                        
            }
        }
        if (canFlip) {
            RaycastHit2D hit = Physics2D.Linecast(groundCheckStart.position, frontGroundCheckEnd.position, avoidCollisionLayer);
            
            if (hit.collider != null) {
                canFlip = false;
                
                direction *= -1;
                StartCoroutine(FlipDelay());
                
                        
            }
        }
        
    }

    IEnumerator FlipDelay() {
        
        yield return new WaitForSeconds(1f);
        canFlip = true;
    }
    

    public void GotHit(float damageDone) {
        canTrigger = false;
        StartCoroutine(SecondAttackDelay());
        health -= damageDone;
        animator.SetTrigger(gotHit);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Particles() {
        particles.SetActive(!particles.activeInHierarchy);
    }

    public int Direction() {
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other) {
       
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && canTrigger) {
            canTrigger = false;
            trigger.enabled = false;
            StartCoroutine(SecondAttackDelay());
            animator.SetTrigger(stabHash);
            playerController.TakeDamage(damage);
        }
        
    }

    //prevents double attack and enables second attack if player stays in trigger
    IEnumerator SecondAttackDelay() {
        yield return new WaitForSeconds(secondAttackDelay);
        canTrigger = true;
        trigger.enabled = true;
    }
}
