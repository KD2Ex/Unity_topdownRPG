using UnityEngine;

[System.Serializable]
public class AIAgentTransition
{
    public AIState from;
    public AIState to;
    public AIPredicate[] conditions;

    public bool CheckConditions(AIAgent agent)
    {
        foreach (var condition in conditions)
        {
            if (!condition.Evaluate(agent)) return false;
        }

        return true;
    }
}