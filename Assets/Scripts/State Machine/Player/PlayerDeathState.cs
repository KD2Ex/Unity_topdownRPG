public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.animator.SetBool(player.hash_death, true);
        player.enabled = false;
        //player.EnableInput(false);
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
    }
}