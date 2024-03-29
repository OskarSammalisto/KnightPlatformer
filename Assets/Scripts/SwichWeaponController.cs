﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichWeaponController : MonoBehaviour {
    public GameObject knight;
   
    private PlayerController playerController;
    
    void Start() {
        playerController = knight.GetComponent<PlayerController>();
    }

   
    void Update() {
        foreach (Touch touch in Input.touches) {
            
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            Physics2D.IgnoreLayerCollision(12, 15, true);
            
            switch (touch.phase) {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == gameObject) {
                        playerController.SwitchWeapon();
                    }
                    break;
            }
        
            
        }
        
        
        
        
    }
}
