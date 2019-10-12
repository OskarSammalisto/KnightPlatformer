using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGhoulController : MonoBehaviour
{

    public List<Transform> ghoulRunPath = new List<Transform>();

    private float rotateDelay = 30;
    
    void Start() {
        transform.position = ghoulRunPath[1].position;
    }

    public List<Transform> GetTransformList() {
        return ghoulRunPath;
    }
   
    void Update() {
//        if (Time.deltaTime%rotateDelay < 1) {
//            if (transform.position.x > player.transform.position.x) {
//                transform.localRotation = Quaternion.Euler(0, 180, 0);
//            }
//            else {
//                transform.localRotation = Quaternion.Euler(0, 0, 0);
//            } 
//        }
    }
}
