using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomPowerupController : MonoBehaviour {
    private GameObject player;
    private PlayerController playerController;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerController = player.GetComponent<PlayerController>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null) {
            playerController.Berserker();
            Destroy(gameObject);
        }
    }
}
