using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemContainer : NetworkBehaviour
{
    public List<string> _itemObjects = new List<string>();
    
    public List<string> itemObjects {
        get => _itemObjects;
        set
        {
            _itemObjects = value;
            OnItemObjectsUpdate();
        }
    }

    public int capacity = 1;

    private NetworkIdentity playerId;

    public ClueManager clueManager;

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
    private Image itemImage;

    [SerializeField]
    private TMP_Text itemName;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private GameObject containerFull;

    [SerializeField]
    private GameObject containerEmpty;

    [SerializeField]
    private GameObject containerUI;

    private OnHover onHover;

    private UITransition uiTransition;

    public int _increment = 0;
    public int increment {
        get => _increment;
        set
        {
            _increment = value;
            OnIncrement();
        }
    }

    public static event Action<ClueCardManagerUI> ItemPickedUp;

    private void Awake()
    {
        LocalPlayerAnnouncement.OnLocalPlayerUpdated += UpdatePlayer;
        onHover = GetComponent<OnHover>();
        uiTransition = GetComponent<UITransition>();
    }

    private void OnItemObjectsUpdate()
    {
        if(itemObjects.Count == 0)
        {
            containerEmpty.SetActive(true);
            containerFull.SetActive(false);
        } else
        {
            containerEmpty.SetActive(false);
            containerFull.SetActive(true);
        }
    }

    private void UpdatePlayer(NetworkIdentity playerId)
    {
        if (playerId)
        {
            this.playerId = playerId;
            clueManager = this.playerId.gameObject.GetComponent<ClueManager>();
        }
    }

    [Server]
    public bool PlaceItem(string io)
    {
        
        if(itemObjects.Count == capacity)
        {
            return false;
        } else
        {
            Debug.Log("Adding to item objects");
            itemObjects.Add(io);
            RpcUpdateList(itemObjects);
            return true;
        }
    }

    [Server]
    public void CallRpc()
    {
        Debug.Log("calling Rpc\n\n");
        if (isServer)
        {
            RpcUpdateList(itemObjects);
        }
    }

    [ClientRpc]
    public void RpcUpdateList(List<string> newList)
    {
        itemObjects = newList;
        SetToStart();
    }

    private void OnDestroy()
    {
        LocalPlayerAnnouncement.OnLocalPlayerUpdated -= UpdatePlayer;
    }

    private void OnInspect()
    {
        if (itemObjects.Count > 0)
        {
            SetToStart();
        }
    }

    private void SetToStart()
    {
        if(itemObjects.Count > 0)
        {
            increment = 0;
        } 
    }

    private void OnIncrement()
    {
        currentItem = Constants.Items.itemDict[itemObjects[increment]];
    }

    public void UpdateDisplayedClueUI()
    {
        Debug.Log("Update display clue ui");
        itemImage.sprite = currentItem.uiSprite;
        itemName.text = currentItem.itemName;
        Debug.Log(clueManager.currentClue + " " + currentItem.itemName);
        if (clueManager.currentClue == currentItem.itemName)
        {
            Debug.Log("ACTION BUTTONENABLED");
            actionButton.gameObject.SetActive(true);
        }
        else
        {
            actionButton.gameObject.SetActive(false);
        }
    }

    [Server]
    public void RemoveItem(int i)
    {
        itemObjects.RemoveAt(i);
        RpcUpdateList(itemObjects);
    }

    [ClientRpc]
    public void RpcReset()
    {
        SetToStart();
    }

    [Command (ignoreAuthority = true)]
    private void CmdRemoveItem(NetworkIdentity netId, string clue, int i)
    {
        if(netId.gameObject.GetComponent<ClueManager>().currentClue == clue)
        {
            RemoveItem(i);
            netId.gameObject.GetComponent<ClueManager>().IterateClue();
        }
    }

    #region Button Callbacks
    public void Close()
    {
        containerUI.SetActive(false);
    }

    public void NextCard()
    {
        Debug.Log("Next card " + increment + " " + itemObjects.Count);
        if (increment < (itemObjects.Count - 1))
        {
            Debug.Log("Incrementing");
            increment++;
        }
    }

    public void PreviousCard()
    {
        if (increment > 0)
        {
            increment--;
        }
    }

    public void ActionButtonClicked()
    {
        CmdRemoveItem(playerId, currentItem.itemName, increment);
    }
    #endregion
}
