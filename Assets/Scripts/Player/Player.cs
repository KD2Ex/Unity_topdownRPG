using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private LockOnTarget LockOnTarget;
    [field:SerializeField] public MeleeWeapon meleeWeapon { get; private set; }
    [field:SerializeField] public Animator animator { get; private set; }
    [field:SerializeField] public Rigidbody2D rb { get; private set; }
    [field:SerializeField] public FloatVariable moveSpeed { get; private set; }
    [SerializeField] private Vector3Data Velocity;
    [field:SerializeField] public float health { get; private set; }

    [field:SerializeField] public PickupUI PickupUI { get; private set; }
    [field:SerializeField] public PlayerSensor PlayerSensor { get; private set; }
    
    [Space]
    
    [Header("Abilities")]
    [SerializeField] private Dash Dash;
    [SerializeField] private Parry Parry;
    
    public Vector2 moveDirection { get; private set; }
    public Vector2 lastMoveDirection { get; private set; }

    private StateMachine stateMachine;

    public readonly int hash_lastX = Animator.StringToHash("LastX");
    public readonly int hash_lastY = Animator.StringToHash("LastY");
    public readonly int hash_X = Animator.StringToHash("X");
    public readonly int hash_Y = Animator.StringToHash("Y");
    public readonly int hash_attack = Animator.StringToHash("Attack");
    public readonly int hash_angle = Animator.StringToHash("Angle");
    public readonly int hash_death = Animator.StringToHash("Dead");

    public bool attackInput;
    private bool isAttacking;

    private Interactable interactable;

    private Camera mainCamera => Camera.main;
    
    PlayerIdleState idleState;
    PlayerMoveState moveState;
    PlayerAttackState attackState;
    PlayerDeathState deathState;
    PlayerDashState dashState;
    PlayerParryState parryState;

    public void SetVelocityData(Vector3 value)
    {
        Velocity.Value = value;
    }
    
    private void Awake()
    {
        if (GameManager.instance.Player == null)
        {
            GameManager.instance.Player = this;
        }
        
        stateMachine = new StateMachine();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        deathState = new PlayerDeathState(this);
        dashState = new PlayerDashState(this);
        parryState = new PlayerParryState(this);
        
        At(idleState, moveState, new FuncPredicate(() => moveDirection.magnitude > 0.01f));
        At(moveState, idleState, new FuncPredicate(() => moveDirection.magnitude <= 0.01f));
        
        At(idleState, attackState, new FuncPredicate(() => attackInput));
        At(moveState, attackState, new FuncPredicate(() => attackInput));

        At(idleState, dashState, new FuncPredicate(() => IsDashing()));
        At(moveState, dashState, new FuncPredicate(() => IsDashing()));
        At(attackState, dashState, new FuncPredicate(() => IsDashing()));
        
        At(dashState, idleState, new FuncPredicate(() => !IsDashing()));
        
        At(idleState, parryState, new FuncPredicate(IsParrying));
        At(moveState, parryState, new FuncPredicate(IsParrying));
        At(parryState, idleState, new FuncPredicate(() => !IsParrying()));
        
        At(attackState, idleState, new FuncPredicate(() => !isAttacking));
        
        AtAny(deathState, new FuncPredicate(() => health <= 0));
        
        stateMachine.SetState(idleState);
    }

    private bool IsDashing()
    {
        if (!Dash) return false;

        return Dash.Dashing;
    }

    private bool IsParrying()
    {
        if (!Parry) return false;

        return Parry.Running;
    }
    
    private void OnEnable()
    {
        input.MoveEvent += MoveInput;
        input.AttackEvent += AttackInput;
        input.InteractEvent += Interact;
        input.DashEvent += ExecuteDash;
        input.ParryEvent += ExecuteParry;
        input.LockEvent += Lock;
    }

    private void OnDisable()
    {
        input.MoveEvent -= MoveInput;
        input.AttackEvent -= AttackInput;
        input.InteractEvent -= Interact;
        input.DashEvent -= ExecuteDash;
        input.ParryEvent -= ExecuteParry;
        input.LockEvent -= Lock;
    }

    private void Lock()
    {
        LockOnTarget.LockToNearest();
    }
    
    public void EnableInput(bool value)
    {
        if (value)
        {
            input.EnablePlayerInput();
        }
        else
        {
            input.DisablePlayerInput();
        }
    }

    void Start()
    {
        OnSceneLoading();
    }
    
    void Update()
    {
        stateMachine.Update();

        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            SaveSystem.Save();
        }
        if (Keyboard.current.f9Key.wasPressedThisFrame)
        {
            SaveSystem.Load();
        }
        
    }
    
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void At(IState from, IState to, IPredicate predicate) => stateMachine.AddTransition(from, to, predicate);
    private void AtAny(IState to, IPredicate predicate) => stateMachine.AddAnyTransition(to, predicate);
    
    private void MoveInput(Vector2 value)
    {
        //Debug.Log($"MOve input in {gameObject.name}, value: {value}");
        moveDirection = value;
        if (value.magnitude < 0.01f) return;
        lastMoveDirection = value;
    }

    private void AttackInput(bool value)
    {
        attackInput = value;
        if (value)
        {
        }
    }

    public void Attacking(bool value) => isAttacking = value;

    public void EndOfAttack()
    {
        Attacking(false);
        meleeWeapon.AttackEnded();
    }

    public Vector2 GetAttackDir()
    {
        if (LockOnTarget.Locked)
        {
            return Vector3.Normalize((LockOnTarget.CrosshairPos - transform.position).normalized);
        }
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (mousePos - transform.position).normalized;
        
        return dir;
    }

    public float GetAngle(Vector2 dir)
    {
        return (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    public void DashToTarget(Vector2 dir, Transform target)
    {
        Dash.Execute(dir, target);
    }

    public void StartWaitCoroutine(float time, Action callback)
    {
        StartCoroutine(Coroutines.WaitFor(time, null, callback));
    }
    
    private void ExecuteDash()
    {
        if (!Dash) return;
        
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        var dir = (mousePos - transform.position);

        /*
        Debug.Log("dir" + dir);
        Debug.Log("dir.normalized" + dir.normalized);
        Debug.Log("V3Normalize dir" + Vector3.Normalize(dir));
        Debug.Log("V3 dir.normalized" + Vector3.Normalize(dir.normalized));
        Debug.Log("dir.normalized.normalized" + dir.normalized.normalized);
        */
        
        
        Dash.Execute(dir.normalized);
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    private void ExecuteParry()
    {
        Parry.Execute();
    }

    public void TakeDamage(float damage)
    {
        if (Parry.Running) return;
        
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
    }

    public void SetInteractable(Interactable interactable)
    {
        this.interactable = interactable;
    }

    private void Interact()
    {
        if (interactable == null) return;
        
        interactable.Interact();
    }


    public void AddItem(Item item, PickupInfo uiInfo)
    {
        Debug.Log(item.name);
        PickupUI.ShowPickupItem(uiInfo);
    }

    public void OnSceneLoading()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (!GameManager.instance.PlayerSpawnPoint) return;
            if (!this) return;
            transform.position = GameManager.instance.PlayerSpawnPoint.position;
        };
    }

    public void Save(ref PlayerSaveData data)
    {
        data.Position = transform.position;
    }

    public void Load(PlayerSaveData data)
    {
        transform.position = data.Position;
    }
}



[System.Serializable]
public struct PlayerSaveData
{
    public Vector3 Position;
}
