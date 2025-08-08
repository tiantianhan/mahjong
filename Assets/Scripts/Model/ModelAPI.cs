using System.Collections.Generic;
using UnityEngine;
using GameModel = Model.Game;
using PlayerModel = Model.Player;
using TileModel = Model.Tile;

public class ModelAPI : MonoBehaviour
{
    [SerializeField]
    private int numPlayers = 4;

    [SerializeField]
    private int numTilesPerHand = 13;

    [SerializeField]
    private Player[] players;

    private Dictionary<string, Player> idToPlayer = new();

    [SerializeField]
    public GameModel gameModel;

    void Awake()
    {
        gameModel = new GameModel(numPlayers, numTilesPerHand);
        LoadStartingDeck();

        //TODO: Dynamically add players as they are spawned
        foreach (Player player in players)
        {
            RegisterPlayer(player.name, player);
        }
    }

    #region Load Tile Models
    void LoadStartingDeck()
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

        gameModel.LoadTiles(startingTiles);
    }

    TileAttributes[] LoadAttributeListFromResources()
    {
        TileAttributes[] tileAttributes = Resources.LoadAll<TileAttributes>("TileAttributes");
        return tileAttributes;
    }

    TileModel GetTileModelFromAttribute(int index, TileAttributes attributes)
    {
        TileModel tileModel = new();
        tileModel.index = index;
        tileModel.type = attributes.type;
        tileModel.number = attributes.number;
        tileModel.order = attributes.order;

        return tileModel;
    }
    #endregion

    #region Any player input
    public void RegisterPlayer(string playerName, Player player)
    {
        if (gameModel.isGameFull())
        {
            Debug.LogWarning("Tried to add player while game is full");
        }
        else
        {
            PlayerModel playerModel = new(gameModel.GetNextPlayerID(), playerName);
            gameModel.AddPlayer(playerModel);
            idToPlayer.Add(playerModel.playerID, player);
        }
    }

    public void Shuffle()
    {
        gameModel.Shuffle();
    }

    public void Deal()
    {
        gameModel.Deal();
        foreach (PlayerModel playerModel in gameModel.GetPlayers())
        {
            NotifyDeal(playerModel);
        }
    }

    #endregion

    #region Player specific output
    public void NotifyDeal(PlayerModel player)
    {
        //TODO
    }
    #endregion
}
