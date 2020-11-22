using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class UI_RoleDisplay : MonoBehaviour
{
    private TMP_Text myText;

    public GamePlayer gamePlayer;

    public void Start()
    {
        myText = GetComponent<TMP_Text>();
        gamePlayer.RoleChanged += OnRoleChange;
        OnRoleChange(gamePlayer.role);
       // myText = GetComponent<TMP_Text>();
    }

    public void OnRoleChange(string role)
    {
        myText.text = role;
    }
    //private NetworkIdentity playerId;
    //private void Start()
    //{
    //    myText = GetComponent<TMP_Text>();
    //    LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
    //   // UpdatePlayer()
    //    //playerId = base.connectionToClient.identity;
    //}

    //private void UpdatePlayer (NetworkIdentity playerId)
    //{
    //    Debug.Log("updating player");
    //    this.playerId = playerId;
    //    try {
    //        string role = playerId.gameObject.GetComponent<GamePlayer>().role;
    //        playerId.gameObject.GetComponent<GamePlayer>().RoleChanged += OnRoleUpdate;
    //        OnRoleUpdate(role);
    //    } catch
    //    {

    //    }

    //}

    //private void OnRoleUpdate(string newRole)
    //{
    //    Debug.Log("updating player3");
    //    myText.text = newRole;
    //}

    //private void OnDestroy()
    //{
    //    playerId.gameObject.GetComponent<GamePlayer>().RoleChanged -= OnRoleUpdate;
    //}
}
