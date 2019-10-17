using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButtonController : MonoBehaviour {

    public GameObject sHandler;
    private SceneHandler sceneHandler;
    
    void Start() {
        DontDestroyOnLoad(gameObject);
        sceneHandler = sHandler.GetComponent<SceneHandler>();
    }

   
    void Update() {
        foreach (Touch touch in Input.touches) {
            
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            
            switch (touch.phase) {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == gameObject) {
                        sceneHandler.ChangeScene("level0");
                    }
                    break;
            }
        
        }
    }
}
