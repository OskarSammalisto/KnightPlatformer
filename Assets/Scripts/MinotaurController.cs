using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : MonoBehaviour {
    
    //objects and components
    private Animator animator;
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D playerRB;
    public BoxCollider2D trigger;
    public BoxCollider2D normalCollider;
    public GameObject finalMenu;
    
    //less update rotation
    private float rotateDelay = 10;
    
    //animator hash strings
    private int speedHash = Animator.StringToHash("speed");
    private int tauntHash = Animator.StringToHash("taunt");
    private int attackHash = Animator.StringToHash("attack");
    private int deadHash = Animator.StringToHash("dead");
    private int deadTriggerHash = Animator.StringToHash("deadTrigger");
    private int takeDamageHash = Animator.StringToHash("takeDamage");
    
    //health
    [SerializeField]
    private float health = 15;
    private bool dead;
    
    //damage
    private float damage = 20f;
    private int hitForce = 700;
    
    //attack radius transforms
    public GameObject att1;
    public GameObject att2;
    public GameObject att3;
    
    //status and constants
    private bool canAttack = true;
    private float secondAttackDelay = 2f;
    private float hitCheckDelay = 0.25f;
    public LayerMask playerLayer;
    


    void Start() {
        finalMenu.SetActive(false);
        dead = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

   
    void Update() {
        if (!dead && Time.deltaTime%rotateDelay < 1) {
            if (transform.position.x > player.transform.position.x) {
                       transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            } 
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!dead && canAttack) {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null) {
                Attack();
                StartCoroutine(HitCheck());
            }



        }
    }

    IEnumerator HitCheck() {
        yield return new WaitForSeconds(hitCheckDelay);
        Vector2 hitVector1 = att1.transform.position;
        Vector2 hitVector2 = att2.transform.position;
        Vector2 hitVector3 = att3.transform.position;
        
        RaycastHit2D hit = Physics2D.Linecast(hitVector1, hitVector2, playerLayer);
        RaycastHit2D hit2 = Physics2D.Linecast(hitVector2, hitVector3, playerLayer);

        if (hit.collider != null) {
            playerController.TakeDamage(damage);
            KnockBack();
            
        }
        else if (hit2.collider != null) {
            playerController.TakeDamage(damage);
            KnockBack();
        }
        
        
    }

    private void KnockBack() {
        playerRB.AddForce(transform.right * hitForce);
    }

    private void Attack() {
        animator.SetTrigger(attackHash);
        trigger.enabled = false;
        canAttack = false;
        StartCoroutine(SecondAttackDelay());

    }
    
    IEnumerator SecondAttackDelay() {
        yield return new WaitForSeconds(secondAttackDelay);
        canAttack = true;
        trigger.enabled = true;
    }

    private void Taunt() {
        animator.SetTrigger(tauntHash);
    }

    public void GotHit(float damage) {
        
//        trigger.enabled = false;
//        canAttack = false;
        //StartCoroutine(SecondAttackDelay());
        health -= damage;
        if (health < 1) {
            Die();
        }
        else {
            animator.SetTrigger(takeDamageHash);
        }
        
    }

    private void Die() {
        animator.SetTrigger(deadTriggerHash);
        dead = true;
        trigger.enabled = false;
        normalCollider.enabled = false;
        finalMenu.SetActive(true);
        //Physics2D.IgnoreLayerCollision(12, 11, true);

        //suspend object activity
    }
    
    
    
}
