using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field:SerializeField] public NavMeshAgent agent { get; protected set; }
    [field:SerializeField] public Animator animator { get; protected set; }
    [field:SerializeField] public float health { get; protected set; }
    
    public Transform playerTransform { get; private set; }
    protected StateMachine stateMachine;
    protected float distanceToPlayer => (playerTransform.position - transform.position).magnitude;

    protected bool isAttacking;
    protected bool attackReady = true;
    protected bool hited;
    protected bool stunned;
    protected bool dead;
    
    public readonly int hash_lastX = Animator.StringToHash("LastX");
    public readonly int hash_lastY = Animator.StringToHash("LastY");
    public readonly int hash_X = Animator.StringToHash("X");
    public readonly int hash_Y = Animator.StringToHash("Y");
    public readonly int hash_attack = Animator.StringToHash("Attack");
    public readonly int hash_angle = Animator.StringToHash("Angle");


    public Vector2 DirectionToPlayer => (playerTransform.position - transform.position).normalized.normalized;
    
    private void Awake()
    {
        playerTransform = FindObjectOfType<Player>()?.transform;
        Debug.Log($"player pos: {playerTransform.position}");
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        InitializeStates();
    }

    protected virtual void InitializeStates()
    {
        
    }

    protected void At(IState from, IState to, IPredicate predicate) => stateMachine.AddTransition(from, to, predicate);
    protected void AtAny(IState to, IPredicate predicate) => stateMachine.AddAnyTransition(to, predicate);

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void TakeDamage(float damage)
    {
        hited = true;
        
        health -= damage;

        if (health <= 0)
        {
            Die();
        } 
    }
    
    public void Stun(float time)
    {
        StartCoroutine(Cooldown(time, (coroutineEnded) => stunned = !coroutineEnded));
    }
    public virtual void Attacking(bool value) => isAttacking = value;

    public void AttackCooldown(float time)
    {
        StartCoroutine(Cooldown(time, (coroutineEnded) => attackReady = coroutineEnded));
    }

    private IEnumerator Cooldown(float seconds, Action<bool> func)
    {
        func(false);
        yield return new WaitForSeconds(seconds);
        func(true);
    }

    private void Die()
    {
        dead = true;
    }

}
