using System.Collections.Generic;
using UnityEngine;
using TileModel = Model.Tile;

/// <summary>
/// Always used to load tile set to ensure tile models are loaded with the same indices everywhere
/// </summary>
public class TileSetLoader
{
    public static List<TileModel> LoadAllTileModels()
    {
        List<TileModel> startingTiles = new();
        TileAttributes[] attrs = LoadAttributeListFromResources();

        int tileIndex = 1;

        foreach (TileAttributes attributes in attrs)
        {
            for (int i = 0; i < attributes.count; i++)
            {
                TileModel tileModel = GetTileModelFromAttribute(tileIndex, attributes);
                startingTiles.Add(tileModel);
                tileIndex++;
            }
        }

        return startingTiles;
    }

    static TileAttributes[] LoadAttributeListFromResources()
    {
        TileAttributes[] tileAttributes = Resources.LoadAll<TileAttributes>("TileAttributes");
        return tileAttributes;
    }

    static TileModel GetTileModelFromAttribute(int index, TileAttributes attributes)
    {
        TileModel tileModel = new();
        tileModel.index = index;
        tileModel.type = attributes.type;
        tileModel.number = attributes.number;
        tileModel.order = attributes.order;

        return tileModel;
    }

    public static Sprite GetTileAssetForModel(TileModel model)
    {
        TileAttributes attributes = GetAttributeForModel(model);
        return Resources.Load<Sprite>("pixel_art_tile_sprites/" + attributes.asset);
    }

    static TileAttributes GetAttributeForModel(TileModel model)
    {
        string attributesPath = GetAttributePathFromNotation(model.GetNotation());
        return Resources.Load<TileAttributes>(attributesPath);
    }

    //TODO Put this function in a separate class that provides paths
    static string GetAttributePathFromNotation(string notation)
    {
        return "TileAttributes/Tile" + notation;
    }
}
