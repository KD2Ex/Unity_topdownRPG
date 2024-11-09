using UnityEngine;

public abstract class AIPredicate : ScriptableObject
{
    public abstract bool Evaluate(AIAgent agent);
}
