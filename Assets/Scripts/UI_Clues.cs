using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class UI_Clues : MonoBehaviour
{
    private TMP_Text myText;

    public ClueManager clueManager;

    public void Start()
    {
        myText = GetComponent<TMP_Text>();
        clueManager.ClueChanged += OnClueChange;
        OnClueChange(clueManager.currentClue);
        //myText = GetComponent<TMP_Text>();
    }

    public void OnClueChange(string newClue)
    {
        myText.text = newClue;
    }
    //private void Awake()
    //{
    //    myText = GetComponent<TMP_Text>();
    //    LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;

    //    // UpdatePlayer()
    //    //playerId = base.connectionToClient.identity;
    //}

    //private void UpdatePlayer(NetworkIdentity playerId)
    //{
    //    if (playerId)
    //    {
    //        this.playerId = playerId;
    //        ClueManager pw = this.playerId.gameObject.GetComponent<ClueManager>();
    //        if (pw)
    //        {
    //            pw.ClueChanged += UpdateClue;
    //            //if (pw.equippedWeapon != null)
    //            //    UpdateWeapon(Constants.WeaponObjects.uiWeaponsDict[pw.equippedWeapon]);
    //            //Debug.Log("Updating ui weapon");
    //            ////WeaponObject weapon = pw.equippedWeapon;
    //            ////UpdateWeapon(weapon);
    //        }
    //    }

    //}

    //private void OnDestroy()
    //{
    //    playerId.gameObject.GetComponent<ClueManager>().ClueChanged -= UpdateClue;

    //    //try
    //    //{
    //    //    playerId.gameObject.GetComponent<ClueManager>().ClueChanged -= UpdateClue;
    //    //    LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
    //    //}
    //    //catch
    //    //{

    //    //}
    //}

    //private void UpdateClue(int clueNumber)
    //{
    //    Debug.Log("updating clue " + clueNumber);
    //    myText.text = "Clues: " + clueNumber;
    //}
}
