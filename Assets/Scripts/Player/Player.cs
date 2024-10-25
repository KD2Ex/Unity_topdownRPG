using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [field:SerializeField] public MeleeWeapon meleeWeapon { get; private set; }
    [field:SerializeField] public Animator animator { get; private set; }
    [field:SerializeField] public Rigidbody2D rb { get; private set; }
    [field:SerializeField] public Dash Dash { get; private set; }
    [field:SerializeField] public float moveSpeed { get; private set; }
    [field:SerializeField] public float health { get; private set; }

    [field:SerializeField] public PickupUI PickupUI { get; private set; }
    
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

    private bool attackInput;
    private bool isAttacking;

    private Interactable interactable;

    private Camera mainCamera => Camera.main;
    
    private void Awake()
    {
        GameManager.instance.Player = this;
        
        stateMachine = new StateMachine();
        
        var idleState = new PlayerIdleState(this);
        var moveState = new PlayerMoveState(this);
        var attackState = new PlayerAttackState(this);
        var deathState = new PlayerDeathState(this);
        var dashState = new PlayerDashState(this);
        
        At(idleState, moveState, new FuncPredicate(() => moveDirection.magnitude > 0.01f));
        At(moveState, idleState, new FuncPredicate(() => moveDirection.magnitude <= 0.01f));
        
        At(idleState, attackState, new FuncPredicate(() => attackInput));
        At(moveState, attackState, new FuncPredicate(() => attackInput));
        
        At(idleState, dashState, new FuncPredicate(() => Dash.Dashing));
        At(moveState, dashState, new FuncPredicate(() => Dash.Dashing));
        At(attackState, dashState, new FuncPredicate(() => Dash.Dashing));
        
        At(dashState, idleState, new FuncPredicate(() => !Dash.Dashing));
        
        At(attackState, idleState, new ActionPredicate(() => !isAttacking, () => attackInput = false));
        
        AtAny(deathState, new FuncPredicate(() => health <= 0));
        
        stateMachine.SetState(idleState);
    }

    private void OnEnable()
    {
        input.MoveEvent += MoveInput;
        input.AttackEvent += AttackInput;
        input.InteractEvent += Interact;
        input.DashEvent += ExecuteDash;
    }

    private void OnDisable()
    {
        input.MoveEvent -= MoveInput;
        input.AttackEvent -= AttackInput;
        input.InteractEvent -= Interact;
        input.DashEvent -= ExecuteDash;
    }

    public void EnableInput(bool value)
    {
        input.EnablePlayerInput(value);
    }

    void Start()
    {
        
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
        moveDirection = value;
        if (value.magnitude < 0.01f) return;
        lastMoveDirection = value;
    }

    private void AttackInput(bool value)
    {
        attackInput = value;
    }

    public void Attacking(bool value) => isAttacking = value;

    public void EndOfAttack()
    {
        Attacking(false);
        meleeWeapon.AttackEnded();
    }

    public Vector2 GetAttackDir()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (mousePos - transform.position).normalized;
        
        
        return dir;
    }

    public float GetAngle(Vector2 dir)
    {
        return (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    public void TakeDamage(float damage)
    {
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

    private void ExecuteDash()
    {
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        var dir = (mousePos - transform.position);

        Debug.Log("dir" + dir);
        Debug.Log("dir.normalized" + dir.normalized);
        Debug.Log("V3Normalize dir" + Vector3.Normalize(dir));
        Debug.Log("V3 dir.normalized" + Vector3.Normalize(dir.normalized));
        Debug.Log("dir.normalized.normalized" + dir.normalized.normalized);
        
        
        Dash.Execute(dir.normalized);
    }

    public void AddItem(Item item, PickupInfo uiInfo)
    {
        Debug.Log(item.name);
        PickupUI.ShowPickupItem(uiInfo);
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
