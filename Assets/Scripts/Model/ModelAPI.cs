using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Managing.Server;
using FishNet.Object;
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
public class ModelAPI : NetworkBehaviour
{
    [SerializeField]
    private int numPlayers = 4;

    [SerializeField]
    private int numTilesPerHand = 13;

    [SerializeField]
    private Player[] players;

    private Dictionary<string, NetworkConnection> playerIDToNetworkConnection = new();

    [SerializeField]
    public GameModel gameModel;

    [SerializeField]
    private TileSetLookup tileSetLookup;

    [SerializeField]
    private NetworkManager networkManager;

    #region Set up
    void Awake()
    {
        //DEBUG: Add dummy players without networking for debugging
        // foreach (Player player in players)
        // {
        //     RegisterPlayer(player.name, player);
        // }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        ServerSetup();
    }

    [Server]
    void ServerSetup()
    {
        Debug.Log("Server Setup");
        gameModel = new GameModel(numPlayers, numTilesPerHand);
        LoadStartingDeck();
        ServerManager serverManager = networkManager.GetComponent<ServerManager>();
        serverManager.OnAuthenticationResult += OnAuthenticationResultHandler;
    }

    [Server]
    void LoadStartingDeck()
    {
        Debug.Log("LoadStartingDeck");
        List<TileModel> startingTiles = TileSetLoader.LoadAllTileModels();
        gameModel.LoadTiles(startingTiles);
    }

    [Server]
    public void OnAuthenticationResultHandler(
        NetworkConnection networkConnection,
        bool authenticated
    )
    {
        Debug.Log("OnAuthenticationResultHandler");
        if (authenticated)
        {
            RegisterConnection("Player " + gameModel.GetNextPlayerID(), networkConnection);
        }
    }

    #endregion

    #region Any player input
    [Server]
    public void RegisterConnection(string playerName, NetworkConnection networkConnection)
    {
        if (gameModel.isGameFull())
        {
            // TODO: Notify players attempting to join when game is full
            // Also handle cases when player connection drops and reconnects
            Debug.LogWarning("Tried to add player while game is full");
        }
        else
        {
            PlayerModel playerModel = new(gameModel.GetNextPlayerID(), playerName);
            gameModel.AddPlayer(playerModel);
            playerIDToNetworkConnection.Add(playerModel.playerID, networkConnection);
            Debug.Log("Registered " + playerName + " client ID " + networkConnection.ClientId);
        }
    }

    [Server]
    public void Shuffle()
    {
        gameModel.Shuffle();
    }

    [Server]
    public void Deal()
    {
        gameModel.Deal();
        foreach (PlayerModel player in gameModel.GetPlayers())
        {
            NetworkConnection connection = GetConnectionForPlayer(player);
            int[] handIndices = GetHandIndicesForPlayer(player);
            NotifyDeal(connection, handIndices);
        }
    }

    #endregion

    #region Player specific output
    public void NotifyDeal(NetworkConnection connection, int[] handIndices)
    {
        Debug.Log(
            "Sending to connection "
                + connection.ClientId
                + " Dealt indices "
                + string.Join(", ", handIndices)
        );

        //TODO NEXT: Make this call over the network
        // List<Tile> handTiles = tileSetLookup.GetTileListForIndices(handIndices);
        // playerView.SetInitialHand(handTiles);
    }

    NetworkConnection GetConnectionForPlayer(PlayerModel player)
    {
        return playerIDToNetworkConnection[player.playerID];
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
