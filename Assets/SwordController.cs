using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
    
    private int fingerID = -1;
    private float startingX;
    private float startingY;
    private float stabOffset = 0.5f;
    private float slashOffset = 0.5f;
    private bool slash = false;

    private String stab = "Stab";
    private String slashSword = "Slash";

    public GameObject knight;
    private Animator knightAnimator;

    private void Start() {
        knightAnimator = knight.GetComponent<Animator>();
    }

    void Update() {
        
        foreach (Touch touch in Input.touches) {
            
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            switch (touch.phase) {
            
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == gameObject) {
                        fingerID = touch.fingerId;
                        startingX = touchPosition.x;
                        startingY = touchPosition.y;

                    }
                    
                    break;
                case TouchPhase.Moved:
                    if (touch.fingerId == fingerID) {
                        if (touchPosition.y >= startingY + slashOffset) {
                            slash = true;
                        }
                        if (touchPosition.x >= startingX +stabOffset) {
                            if (slash) {
                                knightAnimator.SetTrigger(slashSword);
                                fingerID = -1;
                                slash = false;
                            }
                            else {
                                knightAnimator.SetTrigger(stab);
                                fingerID = -1;
                                slash = false;
                            }
                            
                        }else if (touchPosition.x <= startingX -stabOffset) {
                            fingerID = -1;
                            slash = false;
                        }
                    }
                    
                    break;
                case TouchPhase.Ended:
                    fingerID = -1;
                    slash = false;
                    break;
                
            }

        }
        
    }
}
