using TMPro;
using UnityEngine;

public class SlimeAttackState :  EnemyBaseState
{
    private float cooldown;
    private Slime slime;
    private bool attacked;
    
    public SlimeAttackState(Slime enemy, float cooldown) : base(enemy)
    {
        this.cooldown = cooldown;
        slime = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Attacking(true);
        enemy.animator.SetTrigger(enemy.hash_attack);
        attacked = false;
    }

    public override void Update()
    {
        base.Update();
        
        var stateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Attack")) return;
        
        if (stateInfo.normalizedTime > .7f)
        {
            if (!slime.attack.activeInHierarchy && !attacked)
            {
                slime.attack.SetActive(true);
                attacked = true;
            }
        }
        
        if (stateInfo.normalizedTime > .98f) enemy.Attacking(false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        //slime.attack.SetActive(false);
        enemy.AttackCooldown(cooldown);
    }
}