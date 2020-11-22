using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "New Weapon")]
public class WeaponObject : ScriptableObject
{
    public Sprite inGameSprite;
    public Sprite uiSprite;
    public bool muzzleFlash;
    public float damageDistance;
    public GameObject hitEffect;
    public float attackRate;
    //public GameObject attackEffect;
}
