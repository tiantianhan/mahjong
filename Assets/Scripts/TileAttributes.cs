using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information for a type of tile face as a ScriptableObject
/// </summary>
public class TileAttributes : ScriptableObject
{
    public string type;
    public int number;
    public int count;

    public string asset;

    public int order;

    public string GetNotation()
    {
        return number <= 0 ? type : type + number;
    }
}
