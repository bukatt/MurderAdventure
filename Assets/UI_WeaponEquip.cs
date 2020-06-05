using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Mirror;
public class UI_WeaponEquip : MonoBehaviour
{
    private Image myImage;
    private NetworkIdentity playerId;
    private Sprite mySprite;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
        
        // UpdatePlayer()
        //playerId = base.connectionToClient.identity;
    }

    private void UpdatePlayer(NetworkIdentity playerId)
    {
        if (playerId)
        {
            this.playerId = playerId;
            PlayerWeapon pw = this.playerId.gameObject.GetComponent<PlayerWeapon>();
            if (pw)
            {
                pw.WeaponChanged += UpdateWeapon;
                WeaponObject weapon = pw.equippedWeapon;
                UpdateWeapon(weapon);
            }
        }

    }

    private void OnDestroy()
    {
        try
        {
            playerId.gameObject.GetComponent<PlayerWeapon>().WeaponChanged -= UpdateWeapon;
            LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
        } catch
        {

        }
    }

    private void UpdateWeapon(WeaponObject newWeapon)
    {
        Debug.Log("updating weapon");
        myImage.sprite = newWeapon.uiSprite;
    }
}
