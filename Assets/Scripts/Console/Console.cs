using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] private List<ScriptableConsoleCommand> commands;
    
    public bool TryExecute(string alias)
    {
        foreach (var command in commands)
        {
            //var exists = command.Aliases.Items.Exists((name) => name == alias);
            if (!command.HasAlias(alias)) continue;
            if (!command.CanExecute()) continue;
                
            StartCoroutine(QueueCommand(command));
            return true;
        }

        Debug.LogWarning($"Command {alias} not found");
        return false;
    }

    private void LogActions(ConsoleAction action)
    {
        Debug.Log($"Method info: {action.Action}");
    }

    private bool ActionsCheck(List<ConsoleAction> actions)
    {
        foreach (var action in actions)
        {
            Debug.Log(action.Action);
            if (action.Action != null) return true;
        }

        return false;
    }
    
    private IEnumerator QueueCommand(ScriptableConsoleCommand commandToExec)
    {
        yield return new WaitUntil(() => !GameManager.instance.ConsoleOpen);
        commandToExec.Execute();
    }   
}
