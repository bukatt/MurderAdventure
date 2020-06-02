using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GamePlayer : NetworkBehaviour
{
   

    [SyncVar]
    public int index;

    [SyncVar]
    public string role = Constants.Roles.innocent;

    public PlayerControllerCustom playerController;

    private GameManager gameManager;
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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.GameStateChanged += OnGameStateChanged;
    }

    public override void OnStartServer()
    {
        index = connectionToClient.connectionId;
        gameManager.AddGamePlayer(this);
        gameManager.CheckPlayerCount();
    }

    public override void OnStartClient()
    {
        //DontDestroyOnLoad(gameObject);
      //  gameManager.GameStateChanged += OnGameStateChanged;
       // gameManager.AddGamePlayer(this);

        Debug.Log(Room.pendingPlayers.Count);
    }

    public void OnGameStateChanged(string gameState)
    {
        Debug.Log("Gameplayer game state changing");
        if(gameState == Constants.GameStates.inGame && isLocalPlayer)
        {
            GetComponent<PlayerControllerCustom>().enabled = true;
        }
    }

    public override void OnStopClient()
    {
        Room.gamePlayers.RemovePlayer(this);
        gameManager.RemoveGamePlayer(this);
    }

    [Command]
    public void CmdSetMurderer()
    {
        role = Constants.Roles.murderer;
    }
}
