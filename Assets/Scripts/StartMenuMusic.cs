using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMusic : MonoBehaviour {
    private AudioSource audioSource;
    public AudioClip StartMenuSong;
    public AudioClip screenPressed;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(StartMenuSong);
    }

    public void ScreenPressed() {
        audioSource.Stop();
        audioSource.PlayOneShot(screenPressed);
    }
   
}
