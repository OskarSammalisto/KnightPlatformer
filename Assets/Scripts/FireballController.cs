using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireballController : MonoBehaviour {
    private float damage = 10;
    
   
    
    void Start() {
        Physics2D.IgnoreLayerCollision(12, 14, true);
        Physics2D.IgnoreLayerCollision(11, 14, false);
        
       
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
