using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System;

[AddComponentMenu("")]
public class NetworkManagerCustom : NetworkRoomManager
{
   // public List<GamePlayer> gamePlayers { get; } = new List<GamePlayer>();

    public string gameState;

    public event Action GameSceneLoaded;

    public event Action SceneLoaded;

    public event Action RoomSceneLoaded;

    public event Action PlayerNamesChanged;
    
    public GamePlayersList gamePlayers;

    public Dictionary<RoomPlayerCustom, string> playerNames = new Dictionary<RoomPlayerCustom, string>();

    public override void OnRoomStartHost()
    {
        base.OnRoomStartHost();
        //OnGameStateChanged += OnNewGameState;
    }
    //public void OnNewGameState(string newGameState)
    //{
    //    if (newGameState == Constants.GameStates.pregameCountdown)
    //    {
    //        int murderer = UnityEngine.Random.Range(0, gamePlayers.Count - 1);
    //        gamePlayers[murderer].CmdSetMurderer();
    //    }
    //}

    public override void Start()
    {
        base.Start();
       // roomSlotsUpdated += 
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
        Debug.Log(newSceneName);

        //uiManager.changeServerScene(newSceneName);
    }

    public override void OnRoomClientSceneChanged(NetworkConnection conn)
    {
        base.OnRoomClientSceneChanged(conn);
        //if (IsSceneActive(offlineScene))
        //{
        //    uiManager.ChangeClientScene(offlineScene);
        //} else if (IsSceneActive(onlineScene))
        //{
        //    uiManager.ChangeClientScene(offlineScene);
        //} else if (IsSceneActive(GameplayScene))
        //{
        //    uiManager.ChangeClientScene(offlineScene);
        //}
       
    }

    public bool AddPlayerName(string playerName, RoomPlayerCustom id)
    {
        Debug.Log("AddPlayerName");
        if (playerNames.ContainsValue(playerName)){
            return false;
        } else {
            playerNames[id] = playerName;
            id.playerName = playerName;
            PlayerNamesChanged?.Invoke();
            return true;
        }
    }

    public void RemovePlayerName(string playerName, RoomPlayerCustom id)
    {
        if (playerNames.ContainsKey(id))

        {
            Debug.Log("RemovePlayerName");
            playerNames.Remove(id);
        }
    }

    //public override void OnServerAddPlayer(NetworkConnection conn)
    //{
    //    Transform startPos = GetStartPosition();
    //    GameObject player = startPos != null
    //        ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
    //        : Instantiate(playerPrefab);
        
    //    NetworkServer.AddPlayerForConnection(conn, player);
    //}

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        base.OnRoomServerSceneChanged(sceneName);
        if(sceneName == RoomScene)
        {
            Debug.Log("Room Scene loaded up");
            RoomSceneLoaded?.Invoke();
        }

       // uiManager.changeServerScene(sceneName);
    }

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        gamePlayer.GetComponent<GamePlayer>().index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
        gamePlayer.GetComponent<GamePlayer>().playerName = roomPlayer.GetComponent<RoomPlayerCustom>().playerName;
        GameSceneLoaded?.Invoke();
        return true;
    }

    public void GamePlayerInScene()
    {
        Debug.Log(gamePlayers.gamePlayers.Count + " " + roomSlots.Count);
        if (gamePlayers.gamePlayers.Count == roomSlots.Count)
        {
            Debug.Log("gampelay scene all loaded");
            GameSceneLoaded?.Invoke();
        }
    }

    public override void OnRoomStopClient()
    {
        // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
        // going to the offline scene to avoid collision with the one that lives there.
        if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().path != offlineScene)
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        base.OnRoomStopClient();
    }

    public override void OnRoomStopServer()
    {
        // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
        // going to the offline scene to avoid collision with the one that lives there.
        if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().path != offlineScene)
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        base.OnRoomStopServer();
    }

    public override void OnRoomServerPlayersReady()
    {
        pendingPlayers.Clear();
        allPlayersReady = true;
        base.OnRoomServerPlayersReady();
    }
}
