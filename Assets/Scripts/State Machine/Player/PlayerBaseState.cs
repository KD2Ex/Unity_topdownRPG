using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected Player player;

    protected PlayerBaseState(Player player)
    {
        this.player = player;
    }

    public virtual void Enter()
    {
            
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}