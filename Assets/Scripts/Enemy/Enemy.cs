using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field:SerializeField] public NavMeshAgent agent;
    [field:SerializeField] public float health { get; private set; }
    
    private Transform player;
    private StateMachine stateMachine;

    private float distanceToPlayer => (player.position - transform.position).magnitude;

    private bool isAttacking;
    private bool hited;
    private bool stunned;
    private bool dead;

    private float stunTime = 1.5f;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>()?.transform;
        
        stateMachine = new StateMachine();

        var idle = new EnemyIdleState(this);
        var chase = new EnemyChaseState(this);
        var attack = new EnemyAttackState(this);
        var hit = new EnemyHitState(this);
        var death = new EnemyDeathState(this);

        At(idle, chase, new FuncPredicate(() => distanceToPlayer <= 20f));
        At(chase, idle, new FuncPredicate(() => distanceToPlayer > 20f));
        
        At(chase, attack, new FuncPredicate(() => distanceToPlayer < 3f));
        At(attack, idle, new FuncPredicate(() => !isAttacking));
        
        AtAny(hit, new FuncPredicate(() => hited));
        At(hit, idle, new ActionPredicate(() => stunned, () => hited = false));
        
        AtAny(death, new FuncPredicate(() => dead));
        
        stateMachine.SetState(idle);
    }
    
    private void At(IState from, IState to, IPredicate predicate) => stateMachine.AddTransition(from, to, predicate);
    private void AtAny(IState to, IPredicate predicate) => stateMachine.AddAnyTransition(to, predicate);

    public void TakeDamage(float damage)
    {
        hited = true;
        
        health -= damage;

        if (health <= 0)
        {
            Die();
        } 
    }
    
    public void Stun()
    {
        StartCoroutine(BeingStunned());
    }

    private IEnumerator BeingStunned()
    {
        stunned = true;
        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }

    private void Die()
    {
        dead = true;
    }
}
