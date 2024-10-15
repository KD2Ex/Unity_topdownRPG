using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestart : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private InputReader input;

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
        SceneManager.LoadScene(sceneName);
    }
}
