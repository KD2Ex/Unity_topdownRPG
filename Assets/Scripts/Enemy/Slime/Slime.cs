using UnityEngine;

public class Slime : Enemy
{
    private float stunTime = 1.5f;
    
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool infiniteAggroRange;
    [field:SerializeField] public GameObject attack { get; private set; }

    private float hitTime;

    private float selfColliderRadius => Collider2D.bounds.extents.x;
    private float triggerDist => infiniteAggroRange ? float.PositiveInfinity : 20f;
    private float attackRange => (selfColliderRadius > 1f ? selfColliderRadius : 1f) * 2f;
    
    protected override void InitializeStates()
    {
        stateMachine = new StateMachine();

        var idle = new EnemyIdleState(this);
        var stunState = new EnemyStunState(this);
        var chase = new EnemyChaseState(this);
        var attack = new SlimeAttackState(this, attackCooldown);
        var hit = new EnemyHitState(this);
        var death = new EnemyDeathState(this);

        At(idle, chase, new FuncPredicate(() => distanceToPlayer <= triggerDist && distanceToPlayer >= attackRange));
        At(chase, idle, new FuncPredicate(() => distanceToPlayer > triggerDist || distanceToPlayer < attackRange));
        
        At(idle, attack, 
            new FuncPredicate(() => distanceToPlayer < attackRange
            && attackReady));
        At(attack, idle, new FuncPredicate(() => !isAttacking));
        
        AtAny(hit, new FuncPredicate(() => hited && !dead));
        At(hit, idle, new FuncPredicate(() => !hited));
        
        AtAny(death, new FuncPredicate(() => dead));
        
        stateMachine.SetState(idle);

        Debug.Log($"Agent radius: {selfColliderRadius}");
        Debug.Log($"Agent size mag: {Collider2D.bounds.size.magnitude}");
        Debug.Log($"Agent extents mag: {Collider2D.bounds.extents.magnitude}");
        
        Debug.Log("Attack Range  " + attackRange);
        Debug.Log("Trigger Range  " + triggerDist);

        
        
    }

    protected override void Update()
    {
        base.Update();

        
    }
}


