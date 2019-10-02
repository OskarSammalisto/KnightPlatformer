using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject particles;
    [HideInInspector]
    public Animator animator;

    public Transform groundCheckStart;
    public Transform groundCheckEnd;
    public LayerMask avoidCollisionLayer;

    //animator hash
    private int stabHash = Animator.StringToHash("stab");
    
    private float health;
    private float damage = 50f;
    private int direction = 1;
    private bool canFlip = true;

    private int gotHit = Animator.StringToHash("GotHit");
    void Start() {

        health = 5f;
        particles.SetActive(false);
        animator = GetComponent<Animator>();

        HitStateBehaviour hitStateBehaviour = animator.GetBehaviour<HitStateBehaviour>();
        hitStateBehaviour.enemyController = this;

    }

   
    void Update() {

        if (canFlip) {
            RaycastHit2D hit = Physics2D.Linecast(groundCheckStart.position, groundCheckEnd.position, avoidCollisionLayer);
            
            if (hit.collider == null) {
                canFlip = false;
                
                direction *= -1;
                StartCoroutine(FlipDelay());
                
                        
            }
        }
        
    }

    IEnumerator FlipDelay() {
        
        yield return new WaitForSeconds(1f);
        canFlip = true;
    }
    

    public void GotHit(float damage) {
        health -= damage;
        animator.SetTrigger(gotHit);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Particles() {
        particles.SetActive(!particles.activeInHierarchy);
    }

    public int Direction() {
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger enter");
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null) {
            animator.SetTrigger(stabHash);
            playerController.TakeDamage(damage); //TODO: make this a line cast os similar, does double dmg now. perhaps delay attack a bit.
        }
        
    }
}
