using UnityEngine;

public class StartConversation : Interactable
{ 
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private bool interactOnce;
    
    private void OnEnable()
    {
        GameManager.instance.Player.SetInteractable(this);
    }

    public override void Interact()
    {
        if (interactOnce && interacted) return;
        interacted = true;
        
        dialogueBox.SetActive(true);
    }
}
