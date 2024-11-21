using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    private CircleCollider2D triggerArea;
    
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void Awake()
    {
        triggerArea = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnExit?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if (!triggerArea) return;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, triggerArea.radius * 2f);
    }
}