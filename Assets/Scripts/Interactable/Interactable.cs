using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject hint;
    [SerializeField] protected AudioClip clip;
    
    protected Player player;
    protected Animator animator;
    protected bool interacted;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    public abstract void Interact();
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter(other);
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        OnExit(other);
    }
    
    public virtual void OnEnter(Collider2D other)
    {
        if (interacted) return;
        if (!other.CompareTag("Player")) return;
        
        if (hint)
            hint.SetActive(true);
        
        
        player.SetInteractable(this);
    }
    public virtual void OnExit(Collider2D other)
    {
        if (interacted) return;
        if (!other.gameObject.CompareTag("Player")) return;
        
        
        if (hint)
            hint.SetActive(false);
        player.SetInteractable(null);
    }
}