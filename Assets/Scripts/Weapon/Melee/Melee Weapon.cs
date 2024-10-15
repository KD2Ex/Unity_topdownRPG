using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private Collider2D bottom;
    [SerializeField] private Collider2D left;
    [SerializeField] private Collider2D top;
    [SerializeField] private Collider2D right;
    
    private Collider2D[] colliders;

    private void Awake()
    {
        colliders = new[] {bottom, left, top, right};
    }

    public void AttackBottom()
    {
        EnableOnly(bottom);
    }

    public void AttackTop()
    {
        EnableOnly(top);
    }

    public void AttackLeft()
    {
        EnableOnly(left);
    }

    public void AttackRight()
    {
        EnableOnly(right);
    }

    public void Attack(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x);

        dir = Vector3.Normalize(dir);
        
        if (dir.x > .71)
        {
            EnableOnly(right);
        }
        if (dir.y > .71)
        {
            EnableOnly(top);
        }
        if (dir.x < -.71)
        {
            EnableOnly(left);
        }
        if (dir.y < -.71)
        {
            EnableOnly(bottom);
        }
    }
    
    public void AttackEnded()
    {
        DisableAllColliders();
    }
    
    private void EnableOnly(Collider2D collider2D)
    {
        DisableAllColliders();
        collider2D.gameObject.SetActive(true);
    }

    private void DisableAllColliders()
    {
        foreach (var collider in colliders)
        {
            collider.gameObject.SetActive(false);
        }
    }
}
