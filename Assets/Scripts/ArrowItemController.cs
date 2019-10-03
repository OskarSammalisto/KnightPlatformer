using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowItemController : MonoBehaviour {
    private int arrows;
    private int minArrows = 5;
    private int maxArrows = 15;

    private void Start() {
        arrows = Random.Range(minArrows, maxArrows);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null) {
            playerController.PickUpArrows(arrows);
            Destroy(gameObject);
        }
    }
}
