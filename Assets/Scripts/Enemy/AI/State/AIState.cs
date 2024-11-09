using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "SO/AI/State")]
public class AIState : ScriptableObject
{
    [SerializeField] private AIAgentTransition[] transitions;
    [SerializeField] private AIAction[] actions;

    public void UpdateState(AIAgent agent)
    {
        foreach (var aiAction in actions)
        {
            aiAction.Execute(agent);
        }
        
        CheckTransitions(agent);
    }

    public void FixedUpdateState(AIAgent agent)
    {
        foreach (var aiAction in actions)
        {
            aiAction.FixedExecution(agent);
        }
    }
    
    public void CheckTransitions(AIAgent agent)
    {
        foreach (var transition in transitions)
        {
            if (agent.CurrentState != transition.from) continue;
            if (!transition.CheckConditions(agent)) continue;
            
            agent.SetState(transition.to);
            return;
        }
    }
}
