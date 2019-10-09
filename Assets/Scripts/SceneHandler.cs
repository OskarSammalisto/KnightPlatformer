using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    
    AsyncOperation async;
    
    void Start() {
        DontDestroyOnLoad(gameObject);
        
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level1")
        {
            async = SceneManager.LoadSceneAsync("Level2");
            async.allowSceneActivation = false;


        }
        else if (scene.name == "Level2")
        {
            async = SceneManager.LoadSceneAsync("Level1");
            async.allowSceneActivation = false;
        }

    }

    
    public void ChangeScene()
    {
        Debug.Log("change");
        if (async != null)
        {
            Debug.Log("async");
            async.allowSceneActivation = true;
        }

        
    }
   
    void Update() {
        
    }
}
