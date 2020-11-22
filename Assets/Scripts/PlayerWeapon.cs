using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Experimental.Rendering.Universal;


public class PlayerWeapon : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnChangeWeapon))]
    public string equippedWeapon;
   // [SyncVar]
    private List<string> weapons = new List<string>();
    public SpriteRenderer weaponSprite;
    public event Action<WeaponObject> WeaponChanged;
  
    [SerializeField]
    private MuzzleFlash muzzleFlash;

    [SerializeField]
    private ParticleSystem gunSmoke;
  //  [SerializeField]
    

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("starting client");
        SetUp();

    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        equippedWeapon = Constants.WeaponObjects.pistol;
    }

    public void SetUp()
    {
        weapons.Add(Constants.WeaponObjects.pistol);
        weapons.Add(Constants.WeaponObjects.knife);
        //if (weapons.Count > 0)
        //{
        //    CmdChangeWeapon(0);
        //}
    }

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return; }
        if (Input.GetKeyDown("1"))
        {
            CmdChangeWeapon(0);
        } else if (Input.GetKeyDown("2"))
        {
            CmdChangeWeapon(1);
        }
    }

    [Command]
    public void CmdChangeWeapon(int weaponIndex)
    {
        
        if (weapons.Count > weaponIndex)
        {
            equippedWeapon = weapons[weaponIndex];
            //RpcChangeWeapon(weaponIndex);
        }
    }

    [ClientRpc]
    public void RpcAttack()
    {
        Debug.Log(Constants.WeaponObjects.uiWeaponsDict[equippedWeapon].muzzleFlash);
        if (Constants.WeaponObjects.uiWeaponsDict[equippedWeapon].muzzleFlash)
        {
            muzzleFlash.ActivateLight();
            gunSmoke.Play();

        }
    }


    //[ClientRpc]
    //public void RpcChangeWeapon(int weaponIndex)
    //{
    //    equippedWeapon = weapons[weaponIndex];
    //    weaponSprite.sprite = equippedWeapon.inGameSprite;
    //    if (isLocalPlayer)
    //    {
    //        WeaponChanged?.Invoke(equippedWeapon);
    //    }
    //   // weaponSprite.sprite = newEquipped.inGameSprite;
    //}

    public void OnChangeWeapon(string oldWeapon, string newWeapon)
    {
        equippedWeapon = newWeapon;
        WeaponObject weaponObj = Constants.WeaponObjects.uiWeaponsDict[newWeapon];
        WeaponChanged?.Invoke(weaponObj);
        //weaponSprite.sprite = weaponObj.inGameSprite;
    }
}
