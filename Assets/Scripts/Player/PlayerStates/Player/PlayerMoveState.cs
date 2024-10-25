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
        player.rb.MovePosition((Vector2)player.transform.position + player.moveDirection * (player.moveSpeed * Time.deltaTime));
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        player.animator.SetFloat(player.hash_X, 0f);
        player.animator.SetFloat(player.hash_Y, 0f);
    }
}
