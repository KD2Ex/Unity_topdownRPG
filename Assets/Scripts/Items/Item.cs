using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private PickupInfo uiInfo;
    private Player player => GameManager.instance.Player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        player.AddItem(this, uiInfo);
        transform.parent.gameObject.SetActive(false);
    }
}


[Serializable]
public struct PickupInfo
{
    public string Name;
    public string Count;
}