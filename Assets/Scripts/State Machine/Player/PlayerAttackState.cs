using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Attack state");

        var dir = player.GetAttackDir();
        var angle = player.GetAngle(dir);

        Debug.Log(dir);
        Debug.Log(angle);
        
        player.animator.SetFloat(player.hash_angle, angle);
        player.animator.SetTrigger(player.hash_attack);
        player.Attacking(true);
    }

    public override void Update()
    {
        base.Update();
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        player.animator.ResetTrigger(player.hash_attack);
    }
}