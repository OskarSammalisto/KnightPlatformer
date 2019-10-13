using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour {

    public GameObject fireballPrefab;
    
    
    
    public List<Transform> fireballSpawnPoints = new List<Transform>();
   
    private int nextSpawnPoint;



    private float minSpawnTime = 2f;
    private float maxSpawnTime = 6f;
    private float nextSpawn;
    
    void Start() {
        RandomizeSpawn();
        StartCoroutine(Spawner());

    }

    IEnumerator Spawner() {
        while (true) {
        
            yield return new WaitForSeconds(nextSpawn);
            SpawnFireball();
            RandomizeSpawn();
        }
    }

    private void SpawnFireball() {
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoints[nextSpawnPoint].position, fireballSpawnPoints[nextSpawnPoint].rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(10 * fireballSpawnPoints[nextSpawnPoint].right.x, 0);

    }

    private void RandomizeSpawn() {
        nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        nextSpawnPoint = Random.Range(0, fireballSpawnPoints.Count);
    }
}
