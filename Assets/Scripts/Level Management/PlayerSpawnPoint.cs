using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.PlayerSpawnPoint = transform;
    }
}