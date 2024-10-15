using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();


        var dir = player.GetAttackDir();
        var angle = player.GetAngle(dir);
        
        player.animator.SetFloat(player.hash_angle, angle);
        player.animator.SetTrigger(player.hash_attack);
        player.meleeWeapon.Attack(dir);
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