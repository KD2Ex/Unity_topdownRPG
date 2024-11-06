using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private void OnEnable()
    {
        input.EnablePlayerInput(false);
        GameManager.instance.PauseTime();
    }

    private void OnDisable()
    {
        input.EnablePlayerInput(true);
        GameManager.instance.UnpauseTime();
    }
}
