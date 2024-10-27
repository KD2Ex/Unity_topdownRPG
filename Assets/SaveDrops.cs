using System;
using UnityEngine;

public class SaveDrops : MonoBehaviour
{
    [SerializeField] private GameObject sceneRef;
    [SerializeField] private Transform spawnPoint;
    
    private void Awake()
    {
        GameManager.instance.Drops = this;
    }

    public void Save(ref DropsSaveData data)
    {
        data.SceneRef = sceneRef;
    }

    public void Load(DropsSaveData data)
    {
        sceneRef = data.SceneRef;
        Instantiate(sceneRef, spawnPoint.position, Quaternion.identity);
    }
}

[Serializable]
public struct DropsSaveData
{
    public GameObject SceneRef;
}
