using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] private List<ConsoleCommand> commands;
    
    public void TryExecute(string alias)
    {
        foreach (var command in commands)
        {
            var exists = command.CommandAliases.Items.Exists((name) => name.alias == alias);
            if (exists)
            {
                StartCoroutine(QueueCommand(command));
                return;
            }
        }

        Debug.LogWarning($"Command {alias} not found");
    }

    private IEnumerator QueueCommand(ConsoleCommand commandToExec)
    {
        yield return new WaitUntil(() => !GameManager.instance.ConsoleOpen);
        commandToExec.Execute();
    }   
}
