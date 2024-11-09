using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    public EnemyChaseState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //enemy.agent.Move(enemy.transform * 7f * Time.unscaledDeltaTime);
        //enemy.agent.SetDestination(pos);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var dir = enemy.Move();
        var agentVelocity = dir.normalized;
        
        enemy.animator.SetFloat(enemy.hash_X, agentVelocity.x);
        enemy.animator.SetFloat(enemy.hash_Y, agentVelocity.y);
        enemy.animator.SetFloat(enemy.hash_lastX, agentVelocity.x);
        enemy.animator.SetFloat(enemy.hash_lastY, agentVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animator.SetFloat(enemy.hash_X, 0f);
        enemy.animator.SetFloat(enemy.hash_Y, 0f);
    }
}