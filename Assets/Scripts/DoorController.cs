using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    private SceneHandler sceneHandler;

    void Start() {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            sceneHandler.ChangeScene();
        }
    }

    void Update() {
        
        }
}
