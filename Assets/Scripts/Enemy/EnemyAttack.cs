using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;

    private float elapsedTime;

    private void OnEnable()
    {
        elapsedTime = 0f;
    }
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > lifeTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponentInParent<Player>();
        if (!player) return;
        
        player.TakeDamage(damage);
    }
}
