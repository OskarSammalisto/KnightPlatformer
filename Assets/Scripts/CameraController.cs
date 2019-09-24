using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    private float smoothDelay = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform player;

   
    void Update() {
        if (player) {
            Vector3 position = Camera.main.WorldToViewportPoint(player.position);
            Vector3 delta = player.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, position.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothDelay);


        }
        
    }
}
