using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LobbyPlayerTile : NetworkBehaviour
{
    [SyncVar(hook = nameof(ChangeName))]
    public string playerName;

    public bool readyState;

    public TMP_Text nameUI;
    public GameObject waitingUI;
    public GameObject playerJoinedUI;

    public NetworkRoomManager networkRoomManager;

    private void Start()
    {
        networkRoomManager = NetworkManager.singleton as NetworkRoomManager;
    }
    //public void UpdateName(string newName, string oldName)
    //{
    //    if(newName != null && newName != "")
    //    {
    //        if (NetworkManager.IsSceneActive(networkRoomManager.onlineScene))
    //        {
    //            EnableName();
    //        }
    //        nameUI.text = newName;
    //    } else
    //    {
    //        nameUI.text = "";
    //        DisableName();
    //    }
    //}

    public void ChangeName(string oldName, string newName)
    {
       // playerName = newName;
        nameUI.text = newName;
        //Debug.Log("name in change name " + playerName);
        if(newName != null)
        {
            EnableName();
        } else
        {
            DisableName();
        }
    }

    private void EnableName()
    {
        waitingUI.SetActive(false);
        playerJoinedUI.SetActive(true);
    }

    private void DisableName()
    {
        waitingUI.SetActive(true);
        playerJoinedUI.SetActive(false);
    }


}
