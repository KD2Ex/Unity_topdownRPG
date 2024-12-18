 using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Command", menuName = "SO/Console/Commands/Action Command")]
public class ConsoleCommand : ScriptableConsoleCommand
{
    //public CommandAlias CommandAliases;
    public List<ConsoleAction> Actions;

    public override void Execute()
    {
        foreach (var consoleAction in Actions)
        {
            consoleAction.Action?.Invoke();
        }
    }

    public override bool CanExecute()
    {
        foreach (var action in Actions)
        {
            Debug.Log(action.Action);
            if (action.Action != null) return true;
        }

        return false;
    }
}