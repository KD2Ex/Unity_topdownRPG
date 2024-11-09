using UnityEngine;

public class AIAgentSensor : MonoBehaviour
{
    public Transform Target { get; private set; }

    public bool InRange { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Target = other.transform;
        InRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        InRange = false;
    }
}