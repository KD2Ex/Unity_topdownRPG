using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    
    [field:SerializeField] public MeleeWeapon meleeWeapon { get; private set; }
    [field:SerializeField] public Animator animator { get; private set; }
    [field:SerializeField] public Rigidbody2D rb { get; private set; }
    [field:SerializeField] public float moveSpeed { get; private set; }

    public Vector2 moveDirection { get; private set; }
    public Vector2 lastMoveDirection { get; private set; }

    private StateMachine stateMachine;

    public readonly int hash_lastX = Animator.StringToHash("LastX");
    public readonly int hash_lastY = Animator.StringToHash("LastY");
    public readonly int hash_X = Animator.StringToHash("X");
    public readonly int hash_Y = Animator.StringToHash("Y");
    public readonly int hash_attack = Animator.StringToHash("Attack");
    public readonly int hash_angle = Animator.StringToHash("Angle");

    private bool attackInput;
    private bool isAttacking;
    
    
    private void Awake()
    {
        stateMachine = new StateMachine();
        
        var idleState = new PlayerIdleState(this);
        var moveState = new PlayerMoveState(this);
        var attackState = new PlayerAttackState(this);
        
        At(idleState, moveState, new FuncPredicate(() => moveDirection.magnitude > 0.01f));
        At(moveState, idleState, new FuncPredicate(() => moveDirection.magnitude <= 0.01f));
        
        At(idleState, attackState, new FuncPredicate(() => attackInput));
        At(moveState, attackState, new FuncPredicate(() => attackInput));
        
        At(attackState, idleState, new ActionPredicate(() => !isAttacking, () => attackInput = false));
        
        stateMachine.SetState(idleState);
    }

    private void OnEnable()
    {
        input.MoveEvent += MoveInput;
        input.AttackEvent += AttackInput;
    }

    private void OnDisable()
    {
        input.MoveEvent -= MoveInput;
        input.AttackEvent -= AttackInput;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        stateMachine.Update();
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
        var dir = (mousePos - transform.position).normalized.normalized;
        
        
        return dir;
    }

    public float GetAngle(Vector2 dir)
    {
        return (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }
}
