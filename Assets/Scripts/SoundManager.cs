using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private AudioSource audioSource;
    public List<AudioClip> songs = new List<AudioClip>();
    public AudioClip sword;
    public AudioClip shield;
    public AudioClip jump;
    public AudioClip hit;
    
    
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLevelThemeSong(int index) {
        audioSource.PlayOneShot(songs[index]);
    }

    public void StopAudio() {
        audioSource.Stop();
    }

    public void PlaySword() {
        audioSource.PlayOneShot(sword);
    }

    public void PlayShield() {
        audioSource.PlayOneShot(shield);
    }

    public void PlayJump() {
        audioSource.PlayOneShot(jump);
    }

    public void PlayHit() {
        audioSource.PlayOneShot(hit);
    }
    
    



}
