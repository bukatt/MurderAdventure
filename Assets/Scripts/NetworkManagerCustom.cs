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

    public GamePlayersList gamePlayers;

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

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        gamePlayer.GetComponent<GamePlayer>().index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
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

    public override void OnGUI()
    {
        base.OnGUI();
    }


}
