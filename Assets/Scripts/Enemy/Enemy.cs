using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyRuntimeSet set;
    public float originalSpeed { get; private set; }
    public float originalAccel { get; private set; }
    [field:SerializeField] public NavMeshAgent agent { get; protected set; }
    [field:SerializeField] public Animator animator { get; protected set; }
    [field:SerializeField] public Collider2D Collider2D { get; protected set; }
    [field:SerializeField] public Drop Drop { get; protected set; }
    [field:SerializeField] public float health { get; protected set; }

    public Rigidbody2D rb { get; private set; }
    
    public Transform playerTransform => GameManager.instance.Player.transform;
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
    public readonly int hash_hit = Animator.StringToHash("Hit");
    public readonly int hash_death = Animator.StringToHash("Death");


    public Vector2 DirectionToPlayer => (playerTransform.position - transform.position).normalized.normalized;

    public Action<Transform> OnDeath;
    
    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out var rb);
        if (rb) this.rb = rb;
        
        //playerTransform = FindObjectOfType<Player>()?.transform;
        Debug.Log($"player pos: {playerTransform.position}");
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        originalSpeed = agent.speed;
        originalAccel = agent.acceleration;
        
        InitializeStates();
    }

    private void OnEnable()
    {
        Debug.Log($"{gameObject.name} enabled");
        set.Add(this);
    }

    private void OnDisable()
    {
        set.Remove(this);
    }

    protected virtual void InitializeStates()
    {
        
    }

    protected void At(IState from, IState to, IPredicate predicate) => stateMachine.AddTransition(from, to, predicate);
    protected void AtAny(IState to, IPredicate predicate) => stateMachine.AddAnyTransition(to, predicate);

    protected virtual void Update()
    {
        stateMachine.Update();

        //Debug.Log(stateMachine.CurrentState.ToString());
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Took damage");
        
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
        OnDeath?.Invoke(transform);
    }

    public void HitEnded()
    {
        hited = false;
    }

    public Vector3 Move()
    {
        var pos = new Vector3(playerTransform.position.x, playerTransform.position.y, 0f);
        
        if (rb)
        {
            var dir = pos - transform.position;
            rb.MovePosition(transform.position + dir.normalized * (15f * Time.unscaledDeltaTime));

            return dir;
        }

        agent.SetDestination(pos);
        return agent.velocity;
    }
    
}
