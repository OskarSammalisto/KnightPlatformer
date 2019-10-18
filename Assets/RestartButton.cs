using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour {
    public GameObject knight;
    private PlayerController playerController;

    void Start() {
        playerController = knight.GetComponent<PlayerController>();
    }


    void Update() {
        foreach (Touch touch in Input.touches) {
            

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            Debug.Log(hit);


            switch (touch.phase) {
                case TouchPhase.Began:
                    if (hit.collider != null && hit.collider.gameObject == gameObject) {
                        Debug.Log("respawn");
                        playerController.Respawn();
                    }

                    break;
            }
        }
    }
}
