using UnityEngine;

public class AttackCollisionController : MonoBehaviour
{
    [SerializeField] private FloatVariable damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("tirgger enter");
        
        var enemy = other.GetComponentInParent<Enemy>();
        
        Debug.Log($"{other.gameObject.name}");
        if (!enemy) return;
        
        enemy.TakeDamage(damage.Value);
    }
}