using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour {
    private Transform bar;
    void Start() {
        bar = gameObject.transform.GetChild(2);
        
    }


    public void SetHealthBar(float health) {
        bar.localScale = new Vector3(health, 1f);
    }
}
