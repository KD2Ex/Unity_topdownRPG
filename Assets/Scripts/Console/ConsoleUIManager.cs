using UnityEngine;

public class ConsoleUIManager : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject ui;

    private void OnEnable()
    {
        input.PauseEvent += Pause;
    }

    private void OnDisable()
    {
        input.PauseEvent -= Pause;
    }

    private void Pause()
    {
        ui.SetActive(!ui.activeInHierarchy);
    }
}
