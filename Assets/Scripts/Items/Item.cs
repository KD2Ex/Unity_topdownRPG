using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        GameManager.instance.Player.AddItem(this);
        
        transform.parent.gameObject.SetActive(false);
    }
}
