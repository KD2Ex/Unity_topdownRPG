using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    public abstract void Execute(AIAgent agent);
    public abstract void FixedExecution(AIAgent agent);
}
