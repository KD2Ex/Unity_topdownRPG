using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Patrol Action", menuName = "SO/AI/Actions/Patrol")]
public class PatrolAction : AIAction
{
    public override void Execute(AIAgent agent)
    {

    }

    public override void FixedExecution(AIAgent agent)
    {
        Debug.Log($"Patrol action in {agent.name}");
        
        var pos = agent.transform.position * (Random.insideUnitCircle * 3);
        agent.MoveTo(Vector2.right * 10);
    }
}