using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask arrowLayer;
    public Transform startPosition;

    private Rigidbody2D rb;

    private float speed;
    private Vector2 velocity = new Vector2(10f, 3f);

    
    
    //ToDo: destroy arrow at some point
    
    
    void Start() {
        Physics2D.IgnoreLayerCollision(11, 13, true);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    

   
    void FixedUpdate() {

            rb.velocity = velocity;
            velocity.y -= 0.1f;
    
    }
    
   
}
