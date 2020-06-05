using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;


public class PlayerWeapon : NetworkBehaviour
{
    //[SyncVar(hook = nameof(OnChangeWeapon))]
    public WeaponObject equippedWeapon;
    public List<WeaponObject> weapons = new List<WeaponObject>();
    public SpriteRenderer weaponSprite;
    public event Action<WeaponObject> WeaponChanged;
    private void Start()
    {
        if (weapons.Count > 0)
        {
            equippedWeapon = weapons[0];
        }
        weaponSprite.sprite = equippedWeapon.inGameSprite;
    }

    [ClientCallback]
    private void Update()
    {
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
            RpcChangeWeapon(weaponIndex);
        }
    }

    [ClientRpc]
    public void RpcChangeWeapon(int weaponIndex)
    {
        equippedWeapon = weapons[weaponIndex];
        weaponSprite.sprite = equippedWeapon.inGameSprite;
        if (isLocalPlayer)
        {
            WeaponChanged?.Invoke(equippedWeapon);
        }
       // weaponSprite.sprite = newEquipped.inGameSprite;
    }
}
