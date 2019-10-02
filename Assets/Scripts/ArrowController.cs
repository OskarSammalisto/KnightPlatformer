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

    private float arrowLifeTime = 5f;
    private Rigidbody2D rb;
    private bool hasHitGround = false;

    //set arrow damage
    private float arrowDamage = 1f; //possibly set to relative arrow speed + power up modifiers.
    
    void Start() {
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
        }
        
    }

    private void OnTriggerEnter2D(Collider2D objHit) {
       

        
        EnemyController enemyController = objHit.GetComponent<EnemyController>();
        if (enemyController != null) {
            enemyController.GotHit(arrowDamage);
            Destroy(gameObject);
        }
       
    }
}
