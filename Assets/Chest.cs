using UnityEngine;

public class Chest : Interactable
{
    public override void Interact()
    {
        if (interacted) return;
        interacted = true;

        animator.Play("ChestOpen");
        hint.SetActive(false);
    }
}
