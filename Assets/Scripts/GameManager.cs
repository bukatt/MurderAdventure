using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GameManager : NetworkBehaviour
{
    public event Action<string> GameStateChanged;

    [SyncVar]
    public bool allReady = false;

    [SyncVar(hook = nameof(OnGameStateChanged))]
   // [SyncVar]
    public string gameState = "Loading...";
  //  public GamePlayersList gamePlayers;
     public List<GamePlayer> gamePlayerList = new List<GamePlayer>();
   // private Dictionary<string, GamePlayer> players = new Dictionary<string, GamePlayer>();
    private NetworkManagerCustom room;
    private NetworkManagerCustom Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerCustom;
        }
    }

    private void Awake()
    {
       // Room.GameSceneLoaded += OnAllClientsLoaded;
        // DontDestroyOnLoad(gameObject);
    }

    public override void OnStartServer()
    {
        Debug.Log("Starting server");
        base.OnStartServer();
        //CheckPlayerCount();
        Room.GameSceneLoaded += CheckPlayerCount;
        //gamePlayers = Room.gameObject.GetComponent<GamePlayersList>();
        //room.OnGameStateChanged += OnAllClientsLoaded;
       // OnLocalPlayerUpdated?.Invoke(base.netIdentity);
    }

    private void OnDestroy()
    {
        //Room.GameSceneLoadedForAll -= OnAllClientsLoaded;
        // OnLocalPlayerUpdated?.Invoke(null);
    }

    [Server]
    public void CheckPlayerCount()
    {
        Debug.Log("checking player count " + gamePlayerList.Count);
        if (gamePlayerList.Count == Room.roomSlots.Count)
        {
            Debug.Log("Onall clients loaded");
            int murderer = UnityEngine.Random.Range(0, gamePlayerList.Count - 1);
            Debug.Log(gamePlayerList.Count);
            gamePlayerList[murderer].CmdSetMurderer();
            Debug.Log(gameState);
            gameState = Constants.GameStates.inGame;
            RpcGameStateChanged();
            Debug.Log(gameState);
        }
    }

    [ClientRpc]
    public void RpcGameStateChanged()
    {
        GameStateChanged?.Invoke(gameState);
    }

    public void OnGameStateChanged(string oldState, string newState)
    {
        Debug.Log("Game state changed in game manager");
        GameStateChanged?.Invoke(newState);
    }

    public void AddGamePlayer(GamePlayer gp)
    {
        gamePlayerList.Add(gp);
        Debug.Log("AddingGamePlayer");
        //CmdCheckPlayerCount();   
    }

    public void RemoveGamePlayer(GamePlayer gp)
    {
        gamePlayerList.Remove(gp);
    }
}
