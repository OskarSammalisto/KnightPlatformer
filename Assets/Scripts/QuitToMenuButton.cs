using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMenuButton : MonoBehaviour
{
    public List<GameObject> gameObjectsToDestroy = new List<GameObject>();
    public PlayerController playerController;
    
   

   
    void Update() {
        foreach (Touch touch in Input.touches) {
            

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            

            switch (touch.phase) {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == gameObject) {
                        SceneManager.LoadScene("StartingMenu");
                        foreach (GameObject gameObj in gameObjectsToDestroy) {
                            if (gameObj != null) {
                                Destroy(gameObj);
                            }
            
                        }
                    }

                    break;
            }
        }
    }
    
}
