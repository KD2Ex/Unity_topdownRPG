using UnityEngine;

public abstract class ScriptableConsoleCommand : ScriptableObject
{
    public CommandAlias Aliases;
    
    public abstract void Execute();
    public abstract bool CanExecute();
    public virtual bool HasAlias(string alias)
    {
        Debug.Log(name + " has aliases: " + Aliases?.Items);
        if (Aliases == null) return false;
        return Aliases.Items.Exists((name) => name == alias);
    }
}