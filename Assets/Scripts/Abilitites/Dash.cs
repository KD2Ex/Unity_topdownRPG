using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float time;

    private Rigidbody2D rb;
    private WaitForFixedUpdate WaitForFixedUpdate;
    
    public bool Dashing { get; private set; }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    public void Execute(Vector2 dir)
    {
        StartCoroutine(PerformDash(dir, force, time));
    }

    public void Execute(Vector2 dir, float force, float time)
    {
        StartCoroutine(PerformDash(dir, force, time));
    }

    public void Execute(Vector2 dir, Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(DashToTarget(dir, target));
    }

    private IEnumerator PerformDash(Vector2 dir, float force, float time)
    {
        var elapsed = 0f;
        Dashing = true;
        while (elapsed < time)
        {
            elapsed += Time.fixedDeltaTime;
            
            rb.MovePosition(transform.position + (Vector3) dir * (force * Time.fixedDeltaTime));
            
            yield return WaitForFixedUpdate;
        }

        Dashing = false;
    }

    private IEnumerator DashToTarget(Vector2 dir, Transform target)
    {
        float distance;
        //Dashing = true;
        do
        {
            distance = Vector3.Distance(transform.position, target.position);
            Debug.Log(distance);
            rb.MovePosition(transform.position + (Vector3) dir * (force * .5f * Time.deltaTime));
            yield return new WaitForFixedUpdate();
        } while (distance > 2f);
        Debug.Log(distance);
        rb.velocity = Vector3.zero;
        //Dashing = false;
    }
}