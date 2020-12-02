using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;

public class GamePlayer : NetworkBehaviour
{
   

    [SyncVar]
    public int index;

    [SyncVar(hook = nameof(OnChangeName))]
    public string playerName;

    [SyncVar(hook=nameof(OnRoleChange))]
    public string role = Constants.Roles.innocent;

    public event Action<string> RoleChanged;

    public PlayerControllerCustom playerController;

    public GameObject envPS;

    public GameObject flashLight;

    public TMP_Text nameTag;

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

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //gameManager.GameStateChanged += OnGameStateChanged;
        Debug.Log("subbed to gamemanager");
        if (isServer)
        {
            index = connectionToClient.connectionId;
            gameManager.AddGamePlayer(this);
            gameManager.CheckPlayerCount();
        }
        nameTag.text = playerName;
        //GameObject go = Instantiate(envPS, flashLight.transform.position, flashLight.transform.rotation);
        //go.GetComponent<PlayerFollow>().toFollow = flashLight.transform;
    }

    //private void Awake()
    //{
    //    if (isLocalPlayer)
    //    {
    //        GameObject go = GameObject.FindGameObjectWithTag(Constants.Tags.chatManager);
    //        go.GetComponent<Chat>().playerName = playerName;
    //    }
    //}

    public void OnChangeName(string oldName, string newName)
    {
        playerName = newName;
        nameTag.text = playerName;
        if (isLocalPlayer)
        {
            GameObject go = GameObject.FindGameObjectWithTag(Constants.Tags.chatManager);
            go.GetComponent<Chat>().playerName = playerName;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        //index = connectionToClient.connectionId;
        //gameManager.AddGamePlayer(this);
        //gameManager.CheckPlayerCount();
    }

    public override void OnStartClient()
    {
        //DontDestroyOnLoad(gameObject);
        //  gameManager.GameStateChanged += OnGameStateChanged;
        // gameManager.AddGamePlayer(this);
        base.OnStartClient();
        Debug.Log(Room.pendingPlayers.Count);
    }

    //public void OnGameStateChanged(string gameState)
    //{
    //    Debug.Log("Gameplayer game state changing " + gameState);
    //    if(gameState == Constants.GameStates.inGame && isLocalPlayer)
    //    {
    //        GetComponent<PlayerControllerCustom>().enabled = true;
    //    }
    //}

    public override void OnStopClient()
    {
        Room.gamePlayers.RemovePlayer(this);
        gameManager.RemoveGamePlayer(this);
    }

    public void OnRoleChange(string oldRole, string newRole)
    {
        role = newRole;
        RoleChanged?.Invoke(newRole);
    }

    [Command]
    public void CmdSetMurderer()
    {
        role = Constants.Roles.murderer;
    }
}
