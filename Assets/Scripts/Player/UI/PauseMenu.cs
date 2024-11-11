using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    private void OnEnable()
    {
        input.DisablePlayerInput();
        GameManager.instance.PauseTime();
    }

    private void OnDisable()
    {
        input.EnablePlayerInput();
        GameManager.instance.UnpauseTime();
    }
}
