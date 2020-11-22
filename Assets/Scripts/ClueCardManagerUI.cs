using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Mirror;
public class ClueCardManagerUI : NetworkBehaviour
{
    private ItemObject _currentItem;
    public ItemObject currentItem
    {
        get => _currentItem;
        set
        {
            _currentItem = value;
            UpdateDisplayedClueUI();
        }
    }

    [SerializeField]
    private ItemContainer itemContainer;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text itemName;

    private int increment = 0;

    private void OnEnable()
    {
        Debug.Log("enabling clue ui");
        if (itemContainer.itemObjects.Count > 0)
        {
            increment = 0;
            currentItem = Constants.Items.itemDict[itemContainer.itemObjects[0]];
        }
    }

    public void UpdateDisplayedClueUI()
    {
        Debug.Log("Update display clue ui");
        itemImage.sprite = currentItem.uiSprite;
        itemName.text = currentItem.itemName;
    }

    public void NextCard()
    {
        Debug.Log("Next card " + increment + " " + itemContainer.itemObjects.Count);
        if (increment < (itemContainer.itemObjects.Count - 1))
        {
            Debug.Log("Incrementing");
            increment++;
            currentItem = Constants.Items.itemDict[itemContainer.itemObjects[increment]];
        }
    }

    public void PreviousCard()
    {
        if (increment > 0)
        {
            increment--;
            currentItem = Constants.Items.itemDict[itemContainer.itemObjects[increment]];
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    [Command]
    public void CmdRemoveItem()
    {

    }
}
