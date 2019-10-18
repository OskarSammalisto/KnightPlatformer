using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFromEnd : MonoBehaviour {
   
    List<GameObject> destroyList = new List<GameObject>();
    
    void Start() {
       destroyList.Add(GameObject.FindWithTag("Player"));
       destroyList.Add(GameObject.FindWithTag("EventSystem"));
       destroyList.Add(GameObject.FindWithTag("SceneHandler"));
       StartCoroutine(BackToMenu());
    }

    IEnumerator BackToMenu() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartingMenu");
        foreach (GameObject gameObj in destroyList) {
            if (gameObj != null) {
                Destroy(gameObj);
            }
        }
    }

   
//        void Update() {
//            foreach (Touch touch in Input.touches) {
//            
//                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
//                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
//          
//
//                switch (touch.phase) {
//                    case TouchPhase.Began:
//                        if (hit.collider != null && hit.collider.gameObject == gameObject) {
//                            Debug.Log("touch");
//                        
//                            SceneManager.LoadScene("StartingMenu");
//                            foreach (GameObject gameObj in destroyList) {
//                                if (gameObj != null) {
//                                    Destroy(gameObj);
//                                }
//            
//                            }
//                        }
//
//                        break;
//                }
//            }
//        }
}
