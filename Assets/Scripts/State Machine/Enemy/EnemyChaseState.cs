using UnityEngine;

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
        var pos = new Vector3(enemy.playerTransform.position.x, enemy.playerTransform.position.y, 0f);
        enemy.agent.SetDestination(pos);

        var agentVelocity = enemy.agent.velocity.normalized;
        
        enemy.animator.SetFloat(enemy.hash_X, agentVelocity.x);
        enemy.animator.SetFloat(enemy.hash_Y, agentVelocity.y);
        enemy.animator.SetFloat(enemy.hash_lastX, agentVelocity.x);
        enemy.animator.SetFloat(enemy.hash_lastY, agentVelocity.y);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animator.SetFloat(enemy.hash_X, 0f);
        enemy.animator.SetFloat(enemy.hash_Y, 0f);
    }
}