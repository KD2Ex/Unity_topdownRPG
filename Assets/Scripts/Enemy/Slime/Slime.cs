using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Slime : Enemy
{
    private float stunTime = 1.5f;
    [SerializeField] private float attackCooldown = 2f;
    [field:SerializeField] public GameObject attack { get; private set; }

    private float hitTime;
    
    protected override void InitializeStates()
    {
        stateMachine = new StateMachine();

        var idle = new EnemyIdleState(this);
        var stunState = new EnemyStunState(this);
        var chase = new EnemyChaseState(this);
        var attack = new SlimeAttackState(this, attackCooldown);
        var hit = new EnemyHitState(this);
        var death = new EnemyDeathState(this);

        At(idle, chase, new FuncPredicate(() => distanceToPlayer <= 20f && distanceToPlayer >= 2f));
        At(chase, idle, new FuncPredicate(() => distanceToPlayer > 20f || distanceToPlayer < 2f));
        
        At(idle, attack, new FuncPredicate(() => distanceToPlayer < 2f && attackReady));
        At(attack, idle, new FuncPredicate(() => !isAttacking));
        
        AtAny(hit, new FuncPredicate(() => hited && !dead));
        At(hit, idle, new FuncPredicate(() => !hited));
        
        AtAny(death, new FuncPredicate(() => dead));
        
        stateMachine.SetState(idle);
    }
    
}
