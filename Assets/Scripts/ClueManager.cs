using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
using TMPro;

public class ClueManager : NetworkBehaviour
{
    public int cluesFound = 0;
    public int cluesNeeded = 10;
    public event Action<string> ClueChanged;
    public GameObject clueFound;
    public GameObject clueNotFound;
    public Transform speechAnchor;

    public SyncList<string> clueList = new SyncList<string>();

    public SyncList<string> clueListFound = new SyncList<string>();
    private GameManager gameManager;
    
    [SyncVar]
    public string currentClue;
    public string CurrentClue {
        get => currentClue;
        set
        {
            currentClue = value;
            if (isLocalPlayer)
            {
                currentClue_UI.text = currentClue;
            }
        }
    }

    public ItemContainer nearClue;

    #region UI Inputs
    [SerializeField]
    private TMP_Text currentClue_UI;

    #endregion

    private void Awake()
    {
        ClueCardManagerUI.ItemPickedUp += OnItemPickedUp;  
        //gameObject.GetComponent<PlayerControllerCustom>().InspectPressed += InspectClue;
    }

    public void OnItemPickedUp(ClueCardManagerUI ccm)
    {
        ccm.CmdRemoveItem(ccm.increment);
        IterateClue();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < cluesNeeded; i++)
        {
            clueList.Add(RandomItem());
        }
        //gameManager.CheckCluesDistr(this, clueList);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Start()
    {
        if (!isServer) { return; }
        //Debug.Log("hjghhjkjkkjlkjlloloujjkl " + isLocalPlayer);
        IterateClue();
        gameManager.CheckCluesDistr(SyncListAsList());
    }

    private List<string> SyncListAsList()
    {
        List<string> tempList = new List<string>();
        foreach(string c in clueList)
        {
            tempList.Add(c);
        }

        return tempList;
    }

    private string RandomItem()
    {
        return Constants.Items.itemObjects[UnityEngine.Random.Range(0, Constants.Items.itemObjects.Length)].name;
    }

    public void IterateClue()
    {
        if (clueList.Count >= 1)
        {
            string newClue = clueList[0];
            clueList.RemoveAt(0);
            Debug.Log("calling rpc update clue");
            CurrentClue = newClue;
            CmdUpdateClue(newClue);
            clueListFound.Add(newClue);
        } else
        {

        }
    }

    //[Command]
    //public void CmdCheckClue()
    //{
    //    if(nearClue != null)
    //    {
    //        nearClue.TargetOpenUI(connectionToClient);
            
    //    }
    //}

    //[TargetRpc]
    //private void TargetOpenClueUI(NetworkConnection id)
    //{
    //    nearClue.OpenUI();
    //}

    [Command]
    public void CmdUpdateClue(string newClue)
    {   
        currentClue = newClue;
    }



    public void ClueFound()
    {
        //Debug.Log("adding clue");
        //cluesFound++;
        //ClueChanged?.Invoke(cluesFound);
        //GameObject text = Instantiate(clueFound, speechAnchor.position, Quaternion.identity);
        //text.transform.parent = this.transform;
    }

    public void NotClueFound()
    {
        GameObject text = Instantiate(clueNotFound, speechAnchor.position, Quaternion.identity);
        text.transform.parent = this.transform;
    }
}
