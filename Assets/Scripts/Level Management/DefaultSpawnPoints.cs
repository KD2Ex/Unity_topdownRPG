using System;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSpawnPoints : MonoBehaviour
{
    public static DefaultSpawnPoints instance;

    public List<SpawnPoint> Points = new();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        foreach (var point in Points)
        {
            if (Points.FindAll(item => item.SceneName == point.SceneName).Count > 1)
            {
                Points.Remove(point);
            }            
        }
    }
}

[Serializable]
public struct SpawnPoint
{
    public Transform Transform;
    public string SceneName;
}