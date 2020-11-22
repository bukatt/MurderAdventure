using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class ItemContainer : NetworkBehaviour
{
    public List<string> _itemObjects = new List<string>();
    
    public List<string> itemObjects {
        get => _itemObjects;
        set
        {
            _itemObjects = value;
            //if (isServer)
            //{
            //    CallRpc();
            //} else
            //{
            //    UpdateUI();
            //}
        }
    }

    


    public int _moose;
    public int moose {
        get { return _moose; }
        set
        {
            Debug.Log("laksdjflkajsdlfkjas;lkdfj;laksdjflkajsdfkljasdlkfjasldfjalsdkfjlasflkasjflkasjflkasjf");
            _moose = value;
        }
    }

    public List<GameObject> panels = new List<GameObject>();

    //public List<string> itemObjects
    //{
    //    get => _itemObjects;
    //    set {
    //        _itemObjects = value;
    //       // UpdateUI();
    //    }
    //}

    public int capacity = 1;

    [SerializeField]
    private GameObject grid;

    [SerializeField]
    private GameObject panel;



    [Server]
    public bool PlaceItem(string io)
    {
        
        if(itemObjects.Count == capacity)
        {
            return false;
        } else
        {
            Debug.Log("Adding to item objects");
            //var temp = itemObjects;
            //temp.Add(io);
            //itemObjects = temp;
            itemObjects.Add(io);
            RpcUpdateList(itemObjects);
            //RpcUpdateUI();
            moose = 9;
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
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer) { return; }
        Debug.Log("triggering from enter");
        if (collision.transform.tag == Constants.Tags.player)
        {
            ClueManager cm = collision.gameObject.GetComponent<ClueManager>();
            if (cm != null)
            {
                cm.nearClue = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isServer) { return; }
        if (collision.transform.tag == Constants.Tags.player)
        {
            ClueManager cm = collision.gameObject.GetComponent<ClueManager>();
            if (cm != null)
            {
                cm.nearClue = null;
                CloseUI();
            }
        }
        //if (mask == (mask | (1 << collision.gameObject.layer)))
        //{
        //    rend.material.SetInt("_Highlight", 0);
        //    //inspectUI.GetComponent<TMP_Text>().enabled = false;
        //    collision.gameObject.GetComponent<PlayerControllerCustom>().InspectPressed -= this.Activate;
        //    playerObject = null;
        //}
    }

    //private void Awake()
    //{
    //   for(int i = 0; i < capacity; i++)
    //    {
    //        GameObject newPanel = Instantiate(panel);
    //        panel.transform.parent = grid.transform;
    //    }
    //}

    public void OpenUI()
    {
        grid.SetActive(true);
    }

    [TargetRpc]
    public void TargetOpenUI(NetworkConnection connection)
    {
        grid.SetActive(true);
    }

    public void CloseUI()
    {
        grid.SetActive(false);
    }

    public void SelectClue()
    {

    }

    public void UpdateUI()
    {
        Debug.Log("RpcUpdateUI");
        int i = 0;
        foreach (GameObject go in panels)
        {
            if (i < itemObjects.Count)
            {
                go.GetComponent<Image>().sprite = Constants.Items.itemDict[itemObjects[i]].inGameSprite;
            }
            i++;
        }
    }

    [ClientRpc]
    public void RpcUpdateUI()
    {
        int i = 0;
        foreach(GameObject go in panels)
        {
            Debug.Log("For each go in panels" + itemObjects.Count);
            if (i < itemObjects.Count) {
                Debug.Log("Rpc update ui modjlksdjflksdjflkdsjfkl");
                go.GetComponent<Image>().sprite = Constants.Items.itemDict[itemObjects[i]].inGameSprite;
            }
            i++;
        }
    }

    public void RemoveItem(int i)
    {
        itemObjects.RemoveAt(i);
    }
}
