using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string[] scenesToLoad;
    [SerializeField] private string[] scenesToUnload;

    [SerializeField] private string sceneToBeActive;

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
        DontDestroyOnLoad(gameObject);
        StartCoroutine(UnloadAndLoad());
    }

    private IEnumerator UnloadAndLoad()
    {
        foreach (var sceneName in scenesToUnload)
        {
            Debug.Log(sceneName);
            var async = SceneManager.UnloadSceneAsync(sceneName);
            StartCoroutine(LogProgress(async));
            yield return new WaitUntil(() => async.isDone);
        }

        Debug.Log("wtf");
        
        foreach (var sceneName in scenesToLoad)
        {
            Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToBeActive));
        
        Destroy(gameObject);
    }

    private IEnumerator LogProgress(AsyncOperation async)
    {
        Debug.Log(async.progress);
        yield return null;
    } 
}
