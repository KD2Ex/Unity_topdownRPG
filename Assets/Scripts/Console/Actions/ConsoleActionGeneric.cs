using System;
using UnityEngine;

[CreateAssetMenu]
public class ConsoleActionGeneric<T> : ScriptableObject
{
    public Action<T> Action { get; set; }
}