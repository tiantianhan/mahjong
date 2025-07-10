using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Discards : MonoBehaviour
{
    [SerializeField]
    List<Tile> discards;

    public void AddToDiscards(Tile tile)
    {
        discards.Add(tile);

        tile.gameObject.transform.parent = this.transform;
        tile.gameObject.transform.localPosition = Vector3.zero;
    }

    public List<Tile> GetDiscards()
    {
        return discards;
    }
}
