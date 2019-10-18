using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenubutton : MonoBehaviour
{
    AsyncOperation async;
    private BoxCollider2D boxCollider;
    public TMP_Text startText; 
    public StartMenuMusic music;
    
    
    void Start() {

        startText.enabled = false;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        StartCoroutine(StartSequence());
        LoadScene();

    }

    private void Update() {
        foreach (Touch touch in Input.touches) {
            
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            
            switch (touch.phase) {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == gameObject) {
                      
                        ChangeScene();
                    }
                    break;
            }
        
        }
    }

    private void LoadScene() {
        async = SceneManager.LoadSceneAsync("Level0");
        async.allowSceneActivation = false;
    }

    IEnumerator StartSequence() {
        
        yield return new WaitForSeconds(3);
        boxCollider.enabled = true;
        startText.enabled = true;
    }


    private void ChangeScene()
    {
        
        if (async != null)
        {
            async.allowSceneActivation = true;
        }

        
    }

   
}
