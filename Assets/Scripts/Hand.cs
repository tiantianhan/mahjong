using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private List<Tile> tiles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Add(Tile tile)
    {
        tiles.Add(tile);
    }

    void Discard(Tile tile)
    {
        tiles.Remove(tile);
    }
    
    void Order()
    {
        throw new NotImplementedException();
    }

}
