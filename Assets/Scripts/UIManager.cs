using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_InputField ipAddress;
    public NetworkManagerCustom networkManager;
    public GameObject offlineSceneUI;
    public GameObject lobbySceneUI;
    public GameObject inGameUI;
    public DisableEnableUtil toggleGO;

    public LobbyPlayerTile[] playerTiles;

    public GameObject hostStartButton;

    public TMP_InputField playerName;

    public GameObject innocentWinUI;
    public GameObject murdererWinUI;

    public void JoinGame()
    {
        if (ipAddress.text != null)
        {
            networkManager.networkAddress = "localhost";
            networkManager.StartClient();
        }
    }

    //public void RecalculatePlayerTile()
    //{
    //    int i = 0;

    //    for (i = 0; i < playerTiles.Length; i++)
    //    {
    //        if (i < networkManager.roomSlots.Count)
    //        {
    //            playerTiles[i].ChangeName(networkManager.roomSlots[i].GetComponent<RoomPlayerCustom>().playerName);
    //        } else
    //        {
    //            playerTiles[i].ChangeName(null);
    //        }
    //    }
    //}

    //[Command]
    //public void CmdUdpatePlayerTiles()
    //{
    //    UpdatePlayerTiles();
    //}

    //public void UpdatePlayerTiles()
    //{
    //    int i = 0;
    //    Debug.Log(networkManager.roomSlots.Count);
    //    for (i = 0; i < networkManager.roomSlots.Count; i++)
    //    {
    //        Debug.Log("updating tiles");
    //        playerTiles[i].playerName = networkManager.roomSlots[i].GetComponent<RoomPlayerCustom>().playerName;
    //    }
    //}
    #region scene change handlers
    public void ChangeClientScene(string newScene)
    {
        //if(newScene == networkManager.onlineScene)
        //{
        //    CmdUdpatePlayerTiles();
        //}
        Debug.Log("New Scene Name: " + newScene);
        ChangeScene(newScene);

    }

    public void ChangeServerScene(string newScene)
    {
        ChangeScene(newScene);
        if (newScene == networkManager.onlineScene)
        {
            Debug.Log("turning on start button ");
            hostStartButton.SetActive(true);
            //UpdatePlayerTiles();
        }
        else
        {
            hostStartButton.SetActive(false);
        }
    }

   
    public void ChangeToRoom()
    {
        networkManager.ServerChangeScene(networkManager.RoomScene);
    }

    public void ChangeScene(string newScene)
    {

        if (newScene == networkManager.offlineScene)
        {
            if (lobbySceneUI.activeInHierarchy)
            {
                toggleGO.Disable(lobbySceneUI);
            }
            if (inGameUI.activeInHierarchy)
            {
                toggleGO.Disable(inGameUI);
            }
            toggleGO.Enable(offlineSceneUI);

        }
        else if (newScene == networkManager.onlineScene)
        {
            if (offlineSceneUI.activeInHierarchy)
            {
                toggleGO.Disable(offlineSceneUI);
            }
            if (inGameUI.activeInHierarchy)
            {
                toggleGO.Disable(inGameUI);
            }
            
            toggleGO.Enable(lobbySceneUI);
        }
        else if (newScene == networkManager.GameplayScene)
        {
            if (lobbySceneUI.activeInHierarchy)
            {
                toggleGO.Disable(lobbySceneUI);
            }
            if (offlineSceneUI.activeInHierarchy)
            {
                toggleGO.Disable(offlineSceneUI);
            }
            toggleGO.Enable(inGameUI);
        }
        innocentWinUI.SetActive(false);
        murdererWinUI.SetActive(false);
        hostStartButton.SetActive(false);
    }
    #endregion

    #region end game handlers

    public void EnableInnocentsWin()
    {
        Debug.Log("innocents win");
        innocentWinUI.SetActive(true);
    }

    public void EnableMurdererWin()
    {
        Debug.Log("murderer win");
        murdererWinUI.SetActive(true);
    }

    #endregion
}
