using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour {

    public GameObject knight;
    private PlayerController playerController;
    
    private int fingerID = -1;
    private float startingX;
    private float startingY;
    
    
    void Start() {
        playerController = knight.GetComponent<PlayerController>();

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

                    }
                    break;
                case TouchPhase.Ended:
                    if (touch.fingerId == fingerID) {
                        
                        playerController.ShootBow( touchPosition.x - startingX,touchPosition.y - startingY);
                        Debug.Log(touchPosition.y - startingY);
                        fingerID = -1;
                    }
                    break;
            
            }
        
        }
     
    }

  
    
}
