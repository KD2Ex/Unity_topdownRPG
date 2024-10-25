using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MagnetToPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float Delay;

    private float elapsed = 0f;
    private bool start = false;
    
    private Transform Player => GameManager.instance.Player.transform;
    
    public void Execute()
    {
        StartCoroutine(Coroutines.MoveTo(Player, rb, 20f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        StartCoroutine(WaitForDelay());
    }

    private void Update()
    {
        if (start) return;
        
        elapsed += Time.deltaTime;

        if (elapsed >= Delay)
        {
            start = true;
        }
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitUntil(() => start);
        
        Execute();
    }
}