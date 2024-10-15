using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    private float elapsed;
    private float time;
    private AnimatorStateInfo stateInfo;
    
    public EnemyHitState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.animator.SetTrigger(enemy.hash_hit);
        elapsed = 0f;
        time = 0f;
        enemy.Attacking(false);
    }

    public override void Update()
    {
        base.Update();
        stateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Hit")) return;
        Debug.Log(stateInfo.length);
        
        if (Mathf.Approximately(time, 0f))
        {
            time = stateInfo.length;
        }
        if (!stateInfo.IsName("Hit")) return;
        elapsed += Time.deltaTime;
        Debug.Log(elapsed);
        if (elapsed > time)
        {
            enemy.HitEnded();
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