using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    private string levelToLoad;
    private AsyncOperation async;
    
//    AsyncOperation async;
    
    void Start() {
        DontDestroyOnLoad(gameObject);
      
    }
    
    void OnEnable()
    {
        
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;;
    }
    
    void OnDisable()
    {
      
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }


    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1) {
        if( SceneManager.GetActiveScene().name == "LoadingScene")
        {
            StartCoroutine(LoadAndChangeLevel(levelToLoad));
        }
    }
    
     IEnumerator LoadAndChangeLevel(string nextLevel) {
            
         yield return new WaitForSeconds(4);
         
            async = SceneManager.LoadSceneAsync(nextLevel);
            async.allowSceneActivation = false;
    
            yield return new WaitForSeconds(1);
    
           
            async.allowSceneActivation = true;
     }
    
     public void ChangeScene(string nextLevel) {

        
         levelToLoad = nextLevel;
         SceneManager.LoadScene("LoadingScene");
         //        Debug.Log("change");
         //        if (async != null)
         //        {
         //            Debug.Log("async");
         //            async.allowSceneActivation = true;
         //        }


     }
    
//    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
//        //Scene scene = SceneManager.GetActiveScene();
//        
//        if (scene.name == "Level0")
//        {
//            async = SceneManager.LoadSceneAsync("Level1");
//            async.allowSceneActivation = false;
//
//
//        }
//
//        else if (scene.name == "Level1")
//        {
//            async = SceneManager.LoadSceneAsync("Level2");
//            async.allowSceneActivation = false;
//
//
//        }
////        else if (scene.name == "Level2")
////        {
////            async = SceneManager.LoadSceneAsync("Level1");
////            async.allowSceneActivation = false;
////        }
//        
//    }

   

    
   
   
   
}
