public class EnemyBaseState : IState
{
    protected Enemy enemy;
    
    public EnemyBaseState(Enemy enemy)
    {
        this.enemy = enemy;
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