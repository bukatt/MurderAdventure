using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class UI_RoleDisplay : MonoBehaviour
{
    private TMP_Text myText;
    private NetworkIdentity playerId;
    private void Awake()
    {
        myText = GetComponent<TMP_Text>();
        LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
       // UpdatePlayer()
        //playerId = base.connectionToClient.identity;
    }

    private void UpdatePlayer (NetworkIdentity playerId)
    {
        Debug.Log("updating player");
        this.playerId = playerId;
        try {
            string role = playerId.gameObject.GetComponent<GamePlayer>().role;
            OnRoleUpdate(role);
        } catch
        {

        }
        
    }

    private void OnRoleUpdate(string newRole)
    {
        Debug.Log("updating player3");
        myText.text = newRole;
    }
}
