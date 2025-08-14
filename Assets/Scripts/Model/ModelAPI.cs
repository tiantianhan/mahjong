using System.Collections.Generic;
using UnityEngine;
using GameModel = Model.Game;
using PlayerModel = Model.Player;
using TileModel = Model.Tile;

/// <summary>
/// Contains the game model, functions to update model and
/// functions to update listeners of updates to the model
///
///                                      |ModelAPI
/// Players (Listeners) <--- network --> |  |Game Model
///
/// </summary>
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

    #region Set up
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

    void LoadStartingDeck()
    {
        List<TileModel> startingTiles = TileSetLoader.LoadAllTileModels();
        gameModel.LoadTiles(startingTiles);
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
        foreach (PlayerModel player in gameModel.GetPlayers())
        {
            Player playerView = GetViewForPlayer(player);
            int[] handIndices = GetHandIndicesForPlayer(player);
            NotifyDeal(playerView, handIndices);
        }
    }

    #endregion

    #region Player specific output
    //TODO Make this call over the network instead
    public void NotifyDeal(Player playerView, int[] handIndices)
    {
        Debug.Log(
            "Player View "
                + playerView.gameObject.name
                + " Dealt indices "
                + string.Join(", ", handIndices)
        );
    }

    Player GetViewForPlayer(PlayerModel player)
    {
        return idToPlayer[player.playerID];
    }

    int[] GetHandIndicesForPlayer(PlayerModel player)
    {
        List<TileModel> tileModels = player.hand.tiles;
        int[] handIndices = new int[tileModels.Count];
        for (int i = 0; i < handIndices.Length; i++)
        {
            handIndices[i] = tileModels[i].index;
        }

        return handIndices;
    }
    #endregion
}
