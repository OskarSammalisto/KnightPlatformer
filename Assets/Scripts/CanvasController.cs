using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    
    void Start() {
       // DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1) {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }


    private static CanvasController _instance;


//    public static CanvasController Instance { get
//        {
//            if (_instance == null)
//            {
//                _instance = Instantiate(Resources.Load<GameObject>("Canvas")).GetComponent<CanvasController>();
//            }
//
//            return _instance;
//        }
//
//    }
//
//
//    private void Awake()
//    {
//
//        if (_instance != null && _instance != this) {
//            Destroy(gameObject);
//        }
//        else
//        {
//            _instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//    }

   
    void Update() {
        
    }
}
