using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MovingPlatformController : MonoBehaviour
{
    //movement points in Y grid
    private Vector2 currentPosition;
    public Transform upperPosition;
    public Transform lowerPosition;
    bool movingUp;
    private float speed = 1f;

   
    
    void Start() {
        
        currentPosition = transform.position;
        movingUp = true;


    }

   
    void Update() {
      //   transform.position = Vector2.MoveTowards(currentPosition, place.position, speed * Time.deltaTime);

      if (transform.position == upperPosition.position) {
          movingUp = false;
      }

      if (transform.position == lowerPosition.position) {
          movingUp = true;
      }

      if (movingUp) {
          transform.position = Vector2.MoveTowards(currentPosition, upperPosition.position, speed * Time.deltaTime);
      }
      else {
          transform.position = Vector2.MoveTowards(currentPosition, lowerPosition.position, speed * Time.deltaTime);
      }
        
         
    }
}
