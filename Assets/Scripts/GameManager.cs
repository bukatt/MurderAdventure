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
    public GameUIManager UIManager;

    public List<ItemObject> itemList;

    public int clueNumber = 8;

    private GameObject[] containers;
    private List<ItemContainer> containersList = new List<ItemContainer>();
    private ItemObject[] items;
    private bool allCluesDistr = false;
    // private Dictionary<ClueManager, List<string>> cluesDict = new Dictionary<ClueManager, List<string>>();
    private List<List<string>> cluesList = new List<List<string>>();
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

    private void Start()
    {
        UIManager = GameObject.FindGameObjectWithTag(Constants.Tags.gameUIManager).GetComponent<GameUIManager>();
    }

    public override void OnStartServer()
    {
        Debug.Log("Starting server");
        base.OnStartServer();
        //CheckPlayerCount();
        Room.GameSceneLoaded += CheckPlayerCount;
        containers = GameObject.FindGameObjectsWithTag(Constants.Tags.itemContainer);
        foreach(GameObject go in containers)
        {
            containersList.Add(go.GetComponent<ItemContainer>());
        }
        //gamePlayers = Room.gameObject.GetComponent<GamePlayersList>();
        //room.OnGameStateChanged += OnAllClientsLoaded;
        // OnLocalPlayerUpdated?.Invoke(base.netIdentity);
    }

    [Server]
    public void PlaceClues()
    {
        foreach(List<string> items in cluesList)
        {
            foreach(string io in items)
            {
                SelectRandomContainer(Constants.Items.itemDict[io]);
            }
        }
        //foreach(ItemContainer ic in containersList)
        //{
        //    ic.RpcUpdateUI();
        //}
    }

    [Server]
    public void SelectRandomContainer(ItemObject io)
    {
        int randomIndex = UnityEngine.Random.Range(0, containersList.Count);
        Debug.Log(randomIndex + " " + containersList.Count);
        if (!containersList[randomIndex].GetComponent<ItemContainer>().PlaceItem(io.name))
        {
            containersList.RemoveAt(randomIndex);
            SelectRandomContainer(io);
        }
    }

    [Server]
    public void CheckCluesDistr(List<string> playerItems)
    {
        Debug.Log("cmdcheckcluesdistrKLKLKLKLKLKLKLKLKLKLKLKLKLKLKLKL");
        cluesList.Add(playerItems);
        if(cluesList.Count == Room.numPlayers)
        {
            Debug.Log("cmdcheckcluesdistr  place clue");
            PlaceClues();
        }
    }

    private ItemObject RandomItem()
    {
        return itemList[UnityEngine.Random.Range(0, itemList.Count)];
    }

    public void CheckPlayerDeath()
    {
        int liveMurderers = 0;
        int liveInnocents = 0;
        foreach(GamePlayer gp in gamePlayerList)
        {
            if (!gp.gameObject.GetComponent<PlayerDeath>().isDead)
            {
                if (gp.role == Constants.Roles.murderer)
                {
                    liveMurderers++;
                } else
                {
                    liveInnocents++;
                }
            }
        }
        if (liveInnocents == 0 && liveMurderers == 0)
        {
            RpcEnableTie();
        } else if (liveInnocents == 0) 
        {
            RpcEnableMurdererWin();
        } else if(liveMurderers == 0)
        {
            RpcEnableInnocentWin();
        }
        gameState = Constants.GameStates.gameOver;
    }

    #region game over
    [ClientRpc]
    public void RpcEnableInnocentWin()
    {
        UIManager.ShowInnocentWins();
       // Room.uiManager.EnableInnocentsWin();
    }

    [ClientRpc]
    public void RpcEnableMurdererWin()
    {
        UIManager.ShowMurdererWins();
       // Room.uiManager.EnableMurdererWin();
    }

    [ClientRpc]
    public void RpcEnableTie()
    {
        UIManager.ShowTie();
    }
    #endregion

    private void OnDestroy()
    {
        Room.GameSceneLoaded -= CheckPlayerCount;
        // OnLocalPlayerUpdated?.Invoke(null);
    }

    [Server]
    public void CheckPlayerCount()
    {
        Debug.Log("checking player count " + gamePlayerList.Count);
        if (gamePlayerList.Count == Room.roomSlots.Count)
        {
            Debug.Log("Onall clients loaded");
            int murderer = UnityEngine.Random.Range(0, gamePlayerList.Count);
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
        gameState = newState;
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

    [Server]
    public void RestartGame()
    {
        Room.ServerChangeScene(Room.RoomScene);
    }

}
