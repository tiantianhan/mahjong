using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

namespace Model
{
    public class Deck
    {
        private List<Tile> tiles = new();

        public void Shuffle()
        {
            // Fisher-Yates Shuffle. For all but last
            // item, swap item in place with a random item in the
            // remainder of the list
            // Results in equal probability for each permutation of the list
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                int swap_index = Random.Range(i, tiles.Count - 1);
                Tile temp = tiles[i];
                tiles[i] = tiles[swap_index];
                tiles[swap_index] = temp;
            }
        }

        public Tile DrawFromTop()
        {
            Tile tile = tiles[0];
            tiles.RemoveAt(0);
            return tile;
        }

        public void Add(List<Tile> tiles)
        {
            this.tiles.AddRange(tiles);
            Debug.Log("AddRange tiles" + string.Join(", ", this.tiles));
        }

        public List<Tile> GetTiles()
        {
            return new List<Tile>(tiles);
        }
    }
}
