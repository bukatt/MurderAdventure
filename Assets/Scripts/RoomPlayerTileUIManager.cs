using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomPlayerTileUIManager : MonoBehaviour
{
    public NetworkManagerCustom networkManager;
    private LobbyPlayerTile[] playerTiles;
    public void ChangeScene(string newScene)
    {
        if(newScene == networkManager.onlineScene)
        {
            int i = 0;
            //for (i = 0; i < networkManager.roomSlots.Count; i++)
            //{

            //}
        }
    }
}
