using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private VideoSequence sequence;
    
    public override void Interact()
    {
        sequence.PlayNext();
    }
}