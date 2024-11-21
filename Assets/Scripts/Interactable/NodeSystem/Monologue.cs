using UnityEngine;

public class Monologue : MonoBehaviour, IInteractable
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject monologueObject;
    
    
    
    private void OnEnable()
    {
        input.CutsceneInteractEvent += Interact;
    }

    private void OnDisable()
    {
        input.CutsceneInteractEvent -= Interact;
    }
    
    public void Interact()
    {
        monologueObject.SetActive(!monologueObject.activeInHierarchy);
    }
}