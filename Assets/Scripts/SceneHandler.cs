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

        if (scene.name == "TestScene")
        {
            async = SceneManager.LoadSceneAsync("SampleScene");
            async.allowSceneActivation = false;


        }
        else if (scene.name == "SampleScene")
        {
            async = SceneManager.LoadSceneAsync("TestScene");
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
