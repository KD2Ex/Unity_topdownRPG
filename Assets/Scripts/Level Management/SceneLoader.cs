using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToLoad;
    [SerializeField] private SceneField[] scenesToUnload;

    private List<AsyncOperation> asyncOperations = new();
    
    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log(SceneManager.GetSceneAt(i).name);
        }
    }

    private IEnumerator Start()
    {
        yield return null;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log(SceneManager.GetSceneAt(i).name);
            if (SceneManager.GetSceneAt(i).name != "PersistentGameplay")
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneAt(i));
            }
        }
    }

    public void Execute()
    {
        LoadScenes();
        UnloadScenes();
        
        /*DontDestroyOnLoad(gameObject);
        StartCoroutine(UnloadAndLoad());*/
    }

    private IEnumerator UnloadAndLoad()
    {
        foreach (var sceneName in scenesToLoad)
        {
            Debug.Log(sceneName);
            asyncOperations.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            
            /*
            if (sceneName == sceneToBeActive)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToBeActive));
            }*/
        }
        
        for (int i = 0; i < asyncOperations.Count; i++)
        {
            while (!asyncOperations[i].isDone)
            {
                yield return null;
            }
        }

        foreach (var sceneName in scenesToUnload)
        {
            Debug.Log(sceneName);
            var async = SceneManager.UnloadSceneAsync(sceneName);
            StartCoroutine(LogProgress(async));
            yield return new WaitUntil(() => async.isDone);
        }

        Debug.Log("wtf");
        
        Destroy(gameObject);
    }

    private void LoadScenes()
    {
        foreach (var sceneName in scenesToLoad)
        {
            Debug.Log(sceneName);
            asyncOperations.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        }
    }
    
    private void UnloadScenes()
    {
        foreach (var sceneName in scenesToUnload)
        {
            Debug.Log(sceneName);
            var async = SceneManager.UnloadSceneAsync(sceneName);
            //StartCoroutine(LogProgress(async));
        }
    }
    
    private IEnumerator LogProgress(AsyncOperation async)
    {
        Debug.Log(async.progress);
        yield return null;
    } 
}
