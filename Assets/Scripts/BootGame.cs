using UnityEngine;
using UnityEngine.SceneManagement;

public class BootGame : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadSceneAsync("PersistentGameplay");
        var async = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        async.completed += (asyncOp) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        };
    }
}
