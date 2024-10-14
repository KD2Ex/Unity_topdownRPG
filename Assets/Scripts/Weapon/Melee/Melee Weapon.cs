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
        Debug.Log("Bottom Attack");
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
