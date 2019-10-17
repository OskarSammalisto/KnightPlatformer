using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour {

    public GameObject knight;
    private PlayerController playerController;
    public TMP_Text arrowsRemaining;
    
    private int fingerID = -1;
    private float startingX;
    private float startingY;
    
    
    void Start() {
        Physics2D.IgnoreLayerCollision(5, 13, true);
        Physics2D.IgnoreLayerCollision(12, 15, true);
        playerController = knight.GetComponent<PlayerController>();

    }

   
    void Update() {

        arrowsRemaining.text = playerController.ArrowsRemaining().ToString();

        if (playerController.FireArrows()) {
            arrowsRemaining.color = Color.red;
        }
        else {
            arrowsRemaining.color = Color.white;
        }

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

                    }
                    break;
                case TouchPhase.Ended:
                    if (touch.fingerId == fingerID) {
                        
                        playerController.ShootBow( touchPosition.x - startingX,touchPosition.y - startingY);
                        fingerID = -1;
                    }
                    break;
            
            }
        
        }
     
    }

  
    
}
