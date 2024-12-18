using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadSceneCommand", menuName = "Console/LoadSceneCommand")]
public class LoadSceneCommand : ScriptableConsoleCommand
{
    public SceneField SceneField;
    
    public override void Execute()
    {
        SceneManager.LoadSceneAsync("PersistentGameplay");
        SceneManager.LoadSceneAsync(SceneField, LoadSceneMode.Additive);
    }

    public override bool CanExecute()
    {
        return SceneField != null;
    }
}