using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Mirror;
public class UI_WeaponEquip : MonoBehaviour
{
    public Image myImage;
    private NetworkIdentity playerId;
    private Sprite mySprite;

    private void Awake()
    {
        
        LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
        
        // UpdatePlayer()
        //playerId = base.connectionToClient.identity;
    }

    private void Start()
    {
        myImage = GetComponent<Image>();
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
                UpdateWeapon(Constants.WeaponObjects.pistolObject);
                //if (pw.equippedWeapon != null)
                //{
                //    Debug.Log(pw.equippedWeapon);
                //    UpdateWeapon(Constants.WeaponObjects.uiWeaponsDict[pw.equippedWeapon]);
                //}
                //WeaponObject weapon = pw.equippedWeapon;
                //UpdateWeapon(weapon);
            }
        }

    }

    private void OnDestroy() { 


        playerId.gameObject.GetComponent<PlayerWeapon>().WeaponChanged -= UpdateWeapon;
        //try
        //{
        //    playerId.gameObject.GetComponent<PlayerWeapon>().WeaponChanged -= UpdateWeapon;
        //    LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
        //} catch
        //{

        //}
    }

    private void UpdateWeapon(WeaponObject newWeapon)
    {
        Debug.Log("updating weapon" + newWeapon.name);
        myImage.sprite = newWeapon.uiSprite;
    }
}
