using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject particles;
    [HideInInspector]
    public Animator animator;

    private int health;

    private int gotHit = Animator.StringToHash("GotHit");
    void Start() {

        health = 5;
        particles.SetActive(false);
        animator = GetComponent<Animator>();

        HitStateBehaviour hitStateBehaviour = animator.GetBehaviour<HitStateBehaviour>();
        hitStateBehaviour.enemyController = this;

    }

   
    void Update() {
        
    }

    public void GotHit() {
        health--;
        animator.SetTrigger(gotHit);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Particles() {
        particles.SetActive(!particles.activeInHierarchy);
    }
    
    
}
