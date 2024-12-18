using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Alias", menuName = "SO/Console/Alias")]
public class CommandAlias : ScriptableObject
{
    public List<string> Items = new();
}

