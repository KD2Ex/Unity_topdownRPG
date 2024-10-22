using UnityEngine;

public class Chest : Interactable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter(other);
        player.SetInteractable(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnExit(other);
        player.SetInteractable(null);
    }

    public override void Interact()
    {
        if (interacted) return;
        interacted = true;

        animator.Play("ChestOpen");
        hint.SetActive(false);
    }
}
