using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {
    
    //components
    private Animator animator;
    private GameObject player;
    private Transform playerTransform;
    
    //animator hash
    private int spawnHash = Animator.StringToHash("spawn");
    private int dieHash = Animator.StringToHash("die");
    
    //constants
    private float damage = 10;
    private float speed = 2F;
   
    
    void Start() {
        Physics2D.IgnoreLayerCollision(12, 15, true);
        animator = GetComponent<Animator>();
        animator.SetTrigger(spawnHash);
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
    }

   
    void Update() {
        Vector2 current = animator.transform.position;
        Vector2 target = playerTransform.position;

        animator.transform.position = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
        
        if (current.x > target.x) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } 
        
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null) {
            playerController.TakeDamage(damage);
        }

        if (!other.CompareTag("Floor") && !other.CompareTag("joystick")) {
            animator.SetTrigger(dieHash);
        }
        
        
        
    }
}
