using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestart : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private string sceneName;

    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name == "PersistentGameplay") continue;
            sceneName = scene.name;
            break;
        }
    }

    private void OnEnable()
    {
        input.DevEvent += Restart;
    }

    private void OnDisable()
    {
        input.DevEvent -= Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene("PersistentGameplay");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
