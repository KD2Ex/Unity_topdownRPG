using System.Collections.Generic;
using UnityEngine;

public enum Locale
{
    RU,
    EN,
    UA,
    JP,
    CH
}

[CreateAssetMenu]
public class CommandAlias : ScriptableObject
{
    public List<Alias> Items;
}

