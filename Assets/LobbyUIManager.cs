using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using System;

public class LobbyUIManager : NetworkBehaviour
{
    private NetworkManagerCustom networkManager;
    public GameObject startButton;
    public LobbyPlayerTile[] playerTiles;
    //public Dictionary<string, int> playerNames = new Dictionary<string, int>();
    public TMP_InputField nameInput;
    public RoomPlayerCustom networkRoomPlayer;
    public void Awake()
    {
        networkManager = NetworkManager.singleton as NetworkManagerCustom;
        Debug.Log("is server?????: " + isServer);
        if (isServer)
        {
            Debug.Log("is server");
            
        }
       RoomPlayerCustom.RoomPlayerUpdated += SetRoomPlayer;
        
    }

    private void OnDestroy()
    {
        RoomPlayerCustom.RoomPlayerUpdated -= SetRoomPlayer;
        networkManager.PlayerNamesChanged -= PlayerListUpdate;
    }

    public void SetRoomPlayer(RoomPlayerCustom netid)
    {
        networkRoomPlayer = netid;
    }

    public void Start()
    {
        base.OnStartClient();
        Debug.Log("room slots: " + networkManager.roomSlots.Count);
        if (isServer)
        {
            PlayerListUpdate();
        }
            //foreach (RoomPlayerCustom nrp in networkManager.roomslots)
            //{
            //    if (nrp.isLocalPlayer)
            //    {
            //        networkRoomPlayer = nrp;
            //    }
            //}
    }

    public override void OnStartServer()
    {
        Debug.Log("Starting SERVERERERRERER\n\n\n");
        base.OnStartServer();
        networkManager.PlayerNamesChanged += PlayerListUpdate;
        if (startButton)
        {
            Debug.Log("enabling start button");
            startButton.SetActive(true);
        }
        
        // playerNames = networkManager.playerNames;
        //networkManager.roomSlotsUpdated += PlayerListUpdate;
    }

    [Server]
    public void StartGame()
    {
        networkManager.OnRoomServerPlayersReady();
    }

    [Server]
    public void PlayerListUpdate()
    {
        Debug.Log("PlayerListUpdate");
        int i = 0;
        var enumer = networkManager.playerNames.GetEnumerator();
        enumer.MoveNext();
        Debug.Log(networkManager.playerNames.Keys);
        foreach (LobbyPlayerTile lpt in playerTiles)
        {
            Debug.Log("PlayerListUpdate: " + enumer.Current.Value);
            if(i >= networkManager.playerNames.Count)
            {
                lpt.playerName = null;
            } else
            {
                lpt.playerName = enumer.Current.Value;
                enumer.MoveNext();
            }
            i++;
        }
    }

    [Command]
    public void CmdUpdatePlayerName(string newName)
    {
        //playerTiles[id].playerName = newName;
        Debug.Log("CmdUpdatePlayerName");
        if (networkRoomPlayer)
        {
            networkManager.AddPlayerName(newName, networkRoomPlayer);
        } else
        {
            Debug.Log("Failed to update player name");
        }
        
    }

    [Server]
    public void ChangePlayerName(string newName, RoomPlayerCustom rpCustom)
    {
        if (networkRoomPlayer)
        {
            networkManager.AddPlayerName(newName, rpCustom);
        }
        else
        {
            Debug.Log("Failed to update player name");
        }
    }

    public void UpdatePlayerName()
    {
        
        Debug.Log("UpdatePlayerName");
        if (isServer)
        {
            networkManager.AddPlayerName(nameInput.text, networkRoomPlayer);
        } else
        {
            Debug.Log("Bout to update the player name via command");
            networkRoomPlayer.UpdatePlayerName(this, nameInput.text);
        }
        
    }

    [Server]
    public void OnPlayerIndexChange()
    {
        int i = 0;
        //foreach(string name in playerNames.Keys)
        //{
        //    playerTiles[i].playerName = name;
        //}
    }
}
