using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    
    public void Open()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        var waiter = new WaitForFixedUpdate();
        var elapsed = 0f;

        while (elapsed < 1f)
        {
            rb.MovePosition(transform.position + Vector3.up * (speed * Time.fixedDeltaTime));
            elapsed += Time.fixedDeltaTime;
            yield return waiter;
        }
    }
}
