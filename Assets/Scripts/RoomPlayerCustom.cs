using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomPlayerCustom : NetworkRoomPlayer
{
    public GameObject uiCardPrefab;
    private GameObject uiCardGO;
    NetworkManagerCustom networkRoomManager;
    public static event Action<RoomPlayerCustom> RoomPlayerUpdated;
    //[SyncVar(hook = nameof(OnNameChange))]
    //public string playerName;

    private TMP_InputField nameEnterField;
    private Button nameEnterButton;

    public void Awake()
    {
        networkRoomManager = NetworkManager.singleton as NetworkManagerCustom;
        if (isServer)
        {
            networkRoomManager.PlayerNamesChanged += OnPlayerNameChanged;
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        BroadcastSelf();
    }

    private void BroadcastSelf()
    {
        RoomPlayerUpdated?.Invoke(this.gameObject.GetComponent<RoomPlayerCustom>());
    }

    private void OnDestroy()
    {
        networkRoomManager.RoomSceneLoaded -= BroadcastSelf;
        networkRoomManager.PlayerNamesChanged -= OnPlayerNameChanged;
        if (base.isLocalPlayer)
            RoomPlayerUpdated?.Invoke(null);
    }

    public override void OnStartClient()
    {
        // uiCardGO = Instantiate(uiCardPrefab);
        base.OnStartClient();
        
       // uiManager = GameObject.FindGameObjectWithTag(Constants.Tags.uiManager).GetComponent<UIManager>();

        if (isLocalPlayer)
        {
            networkRoomManager.RoomSceneLoaded += BroadcastSelf;
            //CmdChangeName("Player" + (index + 1));
            //  nameEnterField = GameObject.FindGameObjectWithTag(Constants.Tags.nameEnterField).GetComponent<TMP_InputField>();
            //nameEnterButton = GameObject.FindGameObjectWithTag(Constants.Tags.nameEnterButton).GetComponent<Button>();
            //nameEnterButton.onClick.AddListener(OnNameEnterClicked);
            //CmdChangeName("Player" + index);
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("Starting on server");
        base.OnStartServer();
       // networkRoomManager.RoomSceneLoaded += BroadcastSelf;
        networkRoomManager.AddPlayerName("Player" + networkRoomManager.playerNames.Count, this.gameObject.GetComponent<RoomPlayerCustom>());
    }

    [Server]
    public void OnPlayerNameChanged()
    {
        playerName = networkRoomManager.playerNames[this.gameObject.GetComponent<RoomPlayerCustom>()];
    }

    //void OnNameEnterClicked()
    //{
    //    if (nameEnterField.text != null && nameEnterField.text != "")
    //    {
    //        CmdChangeName(nameEnterField.text);
    //    }
    //}

    //public override void OnStartServer()
    //{
    //    base.OnStartServer();
    //    networkRoomManager = NetworkManager.singleton as NetworkManagerCustom;
    //    uiManager = GameObject.FindGameObjectWithTag(Constants.Tags.uiManager).GetComponent<UIManager>();
    //    if (isLocalPlayer)
    //    {
    //        CmdChangeName("Player" + index);
    //    }
    //}

    //[Command]
    //public void CmdChangeName(string newName)
    //{
    //    Debug.Log("changing name " + newName +" for " + index);
    //    if(networkRoomManager.AddPlayerName(newName, index))
    //    {
    //        playerName = newName;
    //        uiManager.playerTiles[index].ChangeName(newName);
    //    }
    //}

    //[Server]
    //public void ChangeName(string newName)
    //{
    //    if (networkRoomManager.AddPlayerName(newName, index))
    //    {
    //        playerName = newName;
    //    }
    //}

    [Server]
    public void ChangeToLobbyScene()
    {

    }

    public void UpdatePlayerName(LobbyUIManager lobbyManager, string newName)
    {
        CmdUpdatePlayerName(newName);
        Debug.Log("Update player name in room player");
    }

    [Command]
    public void CmdUpdatePlayerName(string newName)
    {
        Debug.Log("Cmd update player name ");
        GameObject.FindGameObjectWithTag(Constants.Tags.lobbyUIManager).GetComponent<LobbyUIManager>().ChangePlayerName(newName, this);
    }

    public override void IndexChanged(int oldIndex, int newIndex)
    {
        //if(playerName != "" && playerName != null && hasAuthority)
        //{
        //    CmdChangeName("Player" + (newIndex + 1));
        //}
        Debug.Log("Index Changed");
        base.IndexChanged(oldIndex, newIndex);
        //if (isLocalPlayer)
        //    uiManager.RecalculatePlayerTile();
    }

    //public void OnNameChange(string oldName, string newName)
    //{
    //    if(uiManager == null)
    //    {
    //        uiManager = GameObject.FindGameObjectWithTag(Constants.Tags.uiManager).GetComponent<UIManager>();
    //    }
    //    uiManager.playerTiles[index].ChangeName(newName);
    //}

    public override void OnStopClient()
    {
        base.OnStopClient();
        if (isServer)
        {
            playerName = null;
            networkRoomManager.RemovePlayerName(playerName, this);
        }
    }
}
