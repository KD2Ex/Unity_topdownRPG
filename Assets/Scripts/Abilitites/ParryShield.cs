using UnityEngine;

public class ParryShield : MonoBehaviour
{
    [SerializeField] private GameObject vfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("EnemyAttack")) return;

        Vector3 dir = (other.transform.position - transform.position).normalized;
        Instantiate(vfx, transform.position + dir * 1f, Quaternion.identity);

        var enemy = other.GetComponentInParent<Enemy>();
        
        enemy.TakeDamage(0);
    }
}