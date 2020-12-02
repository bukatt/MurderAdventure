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

    [SerializeField]
    private Button actionButton;

    public int increment = 0;

    public static event Action<ClueCardManagerUI> ItemPickedUp;

    private void OnEnable()
    {
        Debug.Log("enabling clue ui");
        if (itemContainer.itemObjects.Count > 0)
        {
            SetToStart();
        }
    }

    private void SetToStart()
    {
        increment = 0;
        currentItem = Constants.Items.itemDict[itemContainer.itemObjects[0]];
    }

    public void UpdateDisplayedClueUI()
    {
        Debug.Log("Update display clue ui");
        itemImage.sprite = currentItem.uiSprite;
        itemName.text = currentItem.itemName;
        Debug.Log(itemContainer.clueManager.currentClue + " " + currentItem.itemName);
        if (itemContainer.clueManager.currentClue == currentItem.itemName)
        {
            Debug.Log("ACTION BUTTONENABLED");
            actionButton.gameObject.SetActive(true);
        } else
        {
            actionButton.gameObject.SetActive(false);
        }
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
    public void CmdRemoveItem(int i)
    {
            itemContainer.itemObjects.RemoveAt(i);
            RpcReset();
    }

    [ClientRpc]
    public void RpcReset()
    {
        SetToStart();
    }

    public void ActionButtonClicked()
    {
        ItemPickedUp?.Invoke(this);
    }
}
