using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        player.animator.SetFloat(player.hash_X, player.moveDirection.x);
        player.animator.SetFloat(player.hash_Y, player.moveDirection.y);
        
        player.animator.SetFloat(player.hash_lastX, player.lastMoveDirection.x);
        player.animator.SetFloat(player.hash_lastY, player.lastMoveDirection.y);
    }

    public override void FixedUpdate()
    {
        var velocity = player.moveDirection * (player.moveSpeed.Value * Time.deltaTime);
        
        player.rb.MovePosition((Vector2)player.transform.position + velocity);
        player.SetVelocityData(velocity);
        
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocityData(Vector3.zero);
        player.animator.SetFloat(player.hash_X, 0f);
        player.animator.SetFloat(player.hash_Y, 0f);
    }
}
