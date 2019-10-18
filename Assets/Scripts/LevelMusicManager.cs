using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusicManager : MonoBehaviour {
    private SoundManager soundManager;
    private int songIndex;
    
    void Start() {
        StartCoroutine(PlayMusic());
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        
        
    }

    IEnumerator PlayMusic() {
        yield return new WaitForSeconds(1);
        if (SceneManager.GetActiveScene().name == "Level0") {
                    songIndex = 0;
        }
        else if (SceneManager.GetActiveScene().name == "Level1") {
            songIndex = 1;
        }
        else {
            songIndex = 2;
        }
        soundManager.PlayLevelThemeSong(songIndex);
        
        
        
    }

   
   
}
