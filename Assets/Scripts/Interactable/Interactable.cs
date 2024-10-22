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
    public virtual void OnEnter(Collider2D other)
    {
        if (interacted) return;
        if (!other.gameObject.CompareTag("Player")) return;
        
        hint.SetActive(true);
    }
    public virtual void OnExit(Collider2D other)
    {
        if (interacted) return;
        if (!other.gameObject.CompareTag("Player")) return;
        
        hint.SetActive(false);
    }
}