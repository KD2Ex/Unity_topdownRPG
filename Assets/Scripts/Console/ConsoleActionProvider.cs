using UnityEngine;
using UnityEngine.Events;

public class ConsoleActionProvider : MonoBehaviour
{
    [SerializeField] private ConsoleAction consoleAction;
    [SerializeField] private bool executeOnce;

    private bool executed;
    
    public UnityEvent CommandExecute;

    private void OnEnable()
    {
        consoleAction.Action += ActionInvocation;
    }

    private void OnDisable()
    {
        consoleAction.Action -= ActionInvocation;
    }

    private void ActionInvocation()
    {
        if (executed && executeOnce) return;
        CommandExecute.Invoke();
        executed = true;

        //if (executeOnce) enabled = false;
    }
}
