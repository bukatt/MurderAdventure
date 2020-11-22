using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class ItemObject : ScriptableObject
{
    public Sprite inGameSprite;
    public Sprite uiSprite;
    public string itemName;
}
