using System;
using UnityEngine;

public class NPC : MonoBehaviour // Interactable
{
    [SerializeField] private VideoSequence sequence;

    private void Awake()
    {
        sequence.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        sequence.enabled = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        sequence.enabled = false;
    }
    
}