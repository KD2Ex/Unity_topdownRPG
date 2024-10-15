using System;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float cooldown;
    
    public EnemyAttackState(Enemy enemy, float cooldown) : base(enemy)
    {
        this.cooldown = cooldown;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Attacking(true);
        enemy.animator.SetTrigger(enemy.hash_attack);
    }

    public override void Update()
    {
        base.Update();

        var stateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);


        Debug.Log("Attacing");
        
        if (stateInfo.shortNameHash != enemy.hash_attack)
        {
            enemy.Attacking(false);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        enemy.AttackCooldown(cooldown);
    }
}