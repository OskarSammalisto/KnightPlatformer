using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {
    private SceneHandler sceneHandler;
    private string nextLevel;
    private bool triggered;
    private SoundManager soundManager;

    void Start() {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        triggered = false;
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        if (SceneManager.GetActiveScene().name == "Level0") {
            nextLevel = "level1";
        }
        else if (SceneManager.GetActiveScene().name == "Level1") {
            nextLevel = "level2";
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Player") && !triggered) {
            soundManager.StopAudio();
            triggered = true;
            sceneHandler.ChangeScene(nextLevel);
        }
    }

    void Update() {
        
        }
}
