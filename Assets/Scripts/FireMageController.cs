using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageController : MonoBehaviour {
    private float health = 10;

    public GameObject particleSystem;
    public GameObject player;
    public GameObject fireballPrefab;
    private int particleWaitTime = 1;

    private float velocityX;
    private float xMin = 10f;
    private float xMax = 20f;
    private float velocityY;
    private float yMin = 1f;
    private float yMax = 4f;
    
    
    
    void Start() {
        player = GameObject.FindWithTag("Player");
        particleSystem.SetActive(false);
        StartCoroutine(Shoot());
    }

   
    void Update() {
        if (transform.position.x > player.transform.position.x) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator Shoot() {
        
        
        while (true) {
            velocityX = Random.Range(xMin, xMax);
            velocityY = Random.Range(yMin, yMax);
            GameObject fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);
            fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX * -transform.right.x, velocityY);
            yield return new WaitForSeconds(2);
        }

        yield break;
    }
    

    public void GotHit(float damage) {
        health -= damage;
        StartCoroutine(HitEnumarator());
        if (health <= 0) {
            Destroy(gameObject);
        }
        
    }

    IEnumerator HitEnumarator() {
        particleSystem.SetActive(true);
        yield return new WaitForSeconds(particleWaitTime);
        particleSystem.SetActive(false);
    }
}
