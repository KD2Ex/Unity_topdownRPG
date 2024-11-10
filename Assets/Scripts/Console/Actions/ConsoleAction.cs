using System;
using UnityEngine;

[CreateAssetMenu (fileName = "Console Action", menuName = "SO/Console/Action")]
public class ConsoleAction : ScriptableObject
{
    public Action Action { get; set; }
}
