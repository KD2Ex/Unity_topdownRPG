using System;
using UnityEngine;

[CreateAssetMenu]
public class ConsoleAction : ScriptableObject
{
    public Action Action { get; set; }
}
