using UnityEngine;
using UnityEngine.Events;

public class ConsoleActionProvider : MonoBehaviour
{
    [SerializeField] private ConsoleAction consoleAction;

    public UnityEvent CommandExecute;

    private void Awake()
    {
        consoleAction.Action += CommandExecute.Invoke;
    }
 }
