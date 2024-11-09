using UnityEngine;

[CreateAssetMenu(fileName = "In Range Condition", menuName = "SO/AI/Predicates/In Range")]
public class InRangePredicate : AIPredicate
{
    public override bool Evaluate(AIAgent agent) => agent.TargetInChaseSensor;
}