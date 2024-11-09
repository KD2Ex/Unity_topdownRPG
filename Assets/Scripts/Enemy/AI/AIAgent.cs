using System;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    [SerializeField] private AIAgentSensor chaseSensor;

    [SerializeField] private AIState[] states;
    
    private readonly WaitForFixedUpdate fixedWaiter = new();
    private Coroutine movingCoroutine;

    public AIState CurrentState { get; private set; }
    
    public bool TargetInChaseSensor => chaseSensor.InRange;
    public Transform ChaseTarget => chaseSensor.Target;
    
    public void MoveTo(Vector3 position)
    {
        /*if (movingCoroutine != null) StopCoroutine(movingCoroutine);
        movingCoroutine = StartCoroutine(Moving(position));*/
        
        
        var dir = position - transform.position;
        Debug.Log($"{gameObject.name} dir mag: {dir.magnitude}");
        if (dir.magnitude < .05f) return;
        rb.MovePosition(transform.position + Vector3.Normalize(dir) * (moveSpeed * Time.deltaTime));

    }

    public void MoveTo(Transform target)
    {
        /*if (movingCoroutine != null) StopCoroutine(movingCoroutine);
        movingCoroutine = StartCoroutine(Moving(target));*/
        
        var dir = target.position - transform.position;
        if (dir.magnitude > .05f) return;
        rb.MovePosition(transform.position + Vector3.Normalize(dir) * (moveSpeed * Time.deltaTime));


    }

    private IEnumerator Moving(Transform target)
    {
        var magnitude = 0f;

        do
        {
            var dir = target.position - transform.position;
            magnitude = dir.magnitude;
            
            rb.MovePosition(transform.position + Vector3.Normalize(dir) * (moveSpeed * Time.deltaTime));
            yield return fixedWaiter;
        } while (magnitude > 0.05f);
    }
    
    private IEnumerator Moving(Vector3 target)
    {
        var magnitude = 0f;

        do
        {
            var dir = target - transform.position;
            magnitude = dir.magnitude;
            
            rb.MovePosition(transform.position + Vector3.Normalize(dir) * (moveSpeed * Time.deltaTime));
            yield return fixedWaiter;
        } while (magnitude > 0.05f);

        movingCoroutine = null;
    }

    private void Start()
    {
        CurrentState = states[0];
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdateState(this);
    }

    public void SetState(AIState newState)
    {
        CurrentState = newState;
    }
}