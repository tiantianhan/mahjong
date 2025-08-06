using System.Collections.Generic;
using UnityEngine;
using GameModel = Model.Game;
using PlayerModel = Model.Player;

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
    private GameModel gameModel;

    void Awake()
    {
        gameModel = new GameModel(numPlayers, numTilesPerHand);

        //TODO: Dynamically add players as they are spawned
        foreach (Player player in players)
        {
            RegisterPlayer(player.name, player);
        }
    }

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
