using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowItemController : MonoBehaviour
{
    
    private int arrows;
    private int minArrows = 2;
    private int maxArrows = 7;

    private void Start() {
        arrows = Random.Range(minArrows, maxArrows);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null) {
            playerController.PickUpFireArrows(arrows);
            Destroy(gameObject);
        }
    }
}
