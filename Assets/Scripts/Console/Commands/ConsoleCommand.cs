 using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Command", menuName = "SO/Console/Command")]
public class ConsoleCommand : ScriptableObject
{
    public CommandAlias CommandAliases;
    public List<ConsoleAction> Actions;

    public void Execute()
    {
        foreach (var consoleAction in Actions)
        {
            consoleAction.Action?.Invoke();
        }
    }
}