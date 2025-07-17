using System;
using System.Collections.Generic;
using UnityEngine;

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
