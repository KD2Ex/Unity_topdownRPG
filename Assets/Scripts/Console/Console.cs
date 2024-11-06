using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] private List<ConsoleCommand> commands;
    
    public bool TryExecute(string alias)
    {
        foreach (var command in commands)
        {
            var exists = command.CommandAliases.Items.Exists((name) => name.alias == alias);
            if (exists)
            {
                if (!ActionsCheck(command.Actions)) continue;
                
                StartCoroutine(QueueCommand(command));
                return true;
            }
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
    
    private IEnumerator QueueCommand(ConsoleCommand commandToExec)
    {
        yield return new WaitUntil(() => !GameManager.instance.ConsoleOpen);
        commandToExec.Execute();
    }   
}
