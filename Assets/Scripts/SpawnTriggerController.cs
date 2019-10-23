using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnTriggerController : MonoBehaviour {
    public GameObject skeletonPrefab;
    private int spawnHash = Animator.StringToHash("spawn");
    private bool triggered;
    
    
    public List<Transform> spawnPoints = new List<Transform>();
    void Start() {
        foreach (Transform child in transform) {
            spawnPoints.Add(child);
        }

        triggered = false;

    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (!triggered) {
            triggered = true;
            //spawnSkeleton
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
            Instantiate(skeletonPrefab, spawnPoints[randomSpawnPoint].position, transform.rotation);
            Destroy(gameObject);
        }
        
        
    }
}
