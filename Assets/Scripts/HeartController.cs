using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour {
    private float health = 40f;
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null) {
            playerController.GetHealth(health);
        }
        
    }
}
