using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnTriggerController : MonoBehaviour {
    public GameObject skeletonPrefab;
    private int spawnHash = Animator.StringToHash("spawn");
    
    
    public List<Transform> spawnPoints = new List<Transform>();
    void Start() {
        foreach (Transform child in transform) {
            spawnPoints.Add(child);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        
        //spawnSkeleton
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        Instantiate(skeletonPrefab, spawnPoints[randomSpawnPoint].position, transform.rotation);
        Destroy(gameObject);
        
    }
}
