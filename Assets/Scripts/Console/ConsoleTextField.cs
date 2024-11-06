using UnityEngine;

public class ConsoleTextField : MonoBehaviour
{
    [SerializeField] private Console console;

    private void OnEnable()
    {
        GameManager.instance.ConsoleOpen = true;
    }

    private void OnDisable()
    {
        GameManager.instance.ConsoleOpen = false;
    }

    public void EnterCommand(string value)
    {
        Debug.Log($"Console string: {value}");
        console.TryExecute(value);
    }
}