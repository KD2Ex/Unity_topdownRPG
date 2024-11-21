using System;
using UnityEngine;

public class MonologueNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject monologueObject;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        input.CutsceneInteractEvent += Interact;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        input.CutsceneInteractEvent -= Interact;
    }

    public void Interact()
    {
        if (!monologueObject.activeInHierarchy)
        {
            input.DisablePlayerInput();
            monologueObject.SetActive(true);
        }
        else
        {
            input.EnablePlayerInput();
            monologueObject.SetActive(false);
        }
    }
}