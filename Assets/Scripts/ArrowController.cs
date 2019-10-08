using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ArrowController : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask arrowLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    public GameObject particleSystem;

    private float arrowLifeTime = 5f;
    private Rigidbody2D rb;
    private bool hasHitGround = false;

    //set arrow damage
    private float arrowDamage = 1f;
    private float fireArrowDamage = 2f;
    
    void Start() {
        particleSystem = gameObject.transform.GetChild(0).gameObject;
        particleSystem.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(11, 13, true);
        
    }

    private void Update() {
        if (arrowLifeTime <= 0) {
            Destroy(gameObject);
        }

        arrowLifeTime -= Time.deltaTime;
    }
    
    void FixedUpdate()
    {
        
        if (!hasHitGround) {
            transform.Rotate(new Vector3(0, 0, 360.0f - Vector3.Angle(transform.right, rb.velocity.normalized)));
        }

        if (rb.velocity.x <= 0.1f && rb.velocity.x >= -0.1f) {  //TODO: make this better!!! Ok it's better but maybe it can be even better.
            hasHitGround = true;
            arrowDamage = 0;
            

        }
        
    }

    private void OnTriggerEnter2D(Collider2D objHit) {
        string tag = objHit.tag;
         
        switch (tag) {
            case "BlueKnight":
                objHit.GetComponent<EnemyController>().GotHit(arrowDamage);
                Destroy(gameObject);
                break;
             
            case "FireMage":
                objHit.GetComponent<FireMageController>().GotHit(arrowDamage);
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
             
        }

        
//        EnemyController enemyController = objHit.GetComponent<EnemyController>();
//        if (enemyController != null && arrowDamage > 0.1f) {
//            enemyController.GotHit(arrowDamage);
//            Debug.Log("Enemy hit");
//            Destroy(gameObject);
//        }
        
       
    }

    public void IncreaseDamage() {
        arrowDamage = fireArrowDamage;
    }

    public void StartParticles() {
        StartCoroutine(SetParticles());
    }

    //delay so particle system is initialized
     IEnumerator SetParticles() {
        yield return new WaitForSeconds(0.1f);
        particleSystem.SetActive(true);
    }
}
