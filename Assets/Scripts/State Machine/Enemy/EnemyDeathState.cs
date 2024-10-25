using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    private float elapsed;
    private float time;
    
    public EnemyDeathState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        elapsed = 0f;
        time = 0f;
        enemy.animator.SetTrigger(enemy.hash_death);
    }

    public override void Update()
    {
        base.Update();

        var stateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Death")) return;
        elapsed += Time.deltaTime;
        
        if (time == 0f)
        {
            time = stateInfo.length;
        }

        if (elapsed > time)
        {
            enemy.Drop.Spawn();
            enemy.gameObject.SetActive(false);
        }

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