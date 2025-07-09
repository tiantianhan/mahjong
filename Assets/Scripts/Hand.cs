using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void Add(Tile tile)
    {
        tiles.Add(tile);
    }

    public void Discard(Tile tile)
    {
        tiles.Remove(tile);
    }

    public void Order()
    {
        tiles = tiles.OrderBy(t => t.GetOrder()).ToList();
    }

    public void Layout()
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            tiles[i].gameObject.transform.parent = this.transform;
            tiles[i].gameObject.transform.localRotation = Quaternion.identity;
            // TODO: Decouple layout implementation from model of game and game state 
            tiles[i].gameObject.transform.localPosition =  Vector3.right * i * 7/13f;  // Board size / Number of tiles
        }
    }
}
