using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireballController : MonoBehaviour {
    private float damage = 30;
   
    
    void Start() {
        Physics2D.IgnoreLayerCollision(12, 13, true);
       
    }

   
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null) {
            playerController.TakeDamage(damage);
            
        }
        Destroy(gameObject);
    }
}
