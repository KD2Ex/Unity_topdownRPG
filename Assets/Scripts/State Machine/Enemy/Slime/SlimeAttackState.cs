using UnityEngine;

public class SlimeAttackState :  EnemyBaseState
{
    private float cooldown;
    private Slime slime;

    private float elapsedTime;
    
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
    }

    public override void Update()
    {
        base.Update();
        
        var stateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        elapsedTime += Time.deltaTime;

        //if (elapsedTime == 0f) return; // mockTime

        if (!stateInfo.IsName("Attack")) return;
        
        Debug.Log(stateInfo.normalizedTime);
        Debug.Log("shortName " + stateInfo.shortNameHash);
        Debug.Log("enemy Hash " + enemy.hash_attack);
        
        if (stateInfo.normalizedTime > .7f)
        {
            if (!slime.attack.activeInHierarchy)
            {
                slime.attack.SetActive(true);
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
        slime.attack.SetActive(false);
        enemy.AttackCooldown(cooldown);
    }
}