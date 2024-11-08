using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        
        base.Enter();
        if (!enemy.agent.isOnNavMesh) return;
        enemy.agent.ResetPath();
        Debug.Log("Enemy idle state");
    }

    public override void Update()
    {
        base.Update();


        
        var angle = Mathf.Atan2(enemy.DirectionToPlayer.y, enemy.DirectionToPlayer.x) * Mathf.Rad2Deg;
        
        enemy.animator.SetFloat(enemy.hash_angle, angle);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}