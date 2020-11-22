using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MapItem : NetworkBehaviour
{
    [SyncVar]
    public bool isClue = false;

    [SerializeField]
    private LayerMask mask;

    private GameObject inspectUI;

    [SerializeField]
    private GameObject isClueUI;

    [SerializeField]
    private GameObject isNotClueUI;

    public float displayTime = 3f;

    private GameObject playerObject;

    private Renderer rend;

    private void Start()
    {
        //if (!hasAuthority) { return; }
        Debug.Log("setting inspector ui");
        inspectUI = GameObject.FindGameObjectWithTag(Constants.Tags.inspectorUI);
        rend = gameObject.GetComponent<Renderer>();
        isClueUI = GameObject.FindGameObjectWithTag(Constants.Tags.clueUI);
        isNotClueUI = GameObject.FindGameObjectWithTag(Constants.Tags.notClueUI);
    }

    public override void OnStartServer()
    {

        base.OnStartServer();
        if(Random.value > .25)
        {
            isClue = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggering");
        if (mask == (mask | (1 << collision.gameObject.layer)))
        {
            rend.material.SetInt("_Highlight", 1);
            //inspectUI.GetComponent<TMP_Text>().enabled = true;
            if(playerObject == null)
                collision.gameObject.GetComponent<PlayerControllerCustom>().InspectPressed += this.Activate;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("triggering");
        if (mask == (mask | (1 << collision.gameObject.layer)))
        {
            rend.material.SetInt("_Highlight", 0);
            //inspectUI.GetComponent<TMP_Text>().enabled = false;
            collision.gameObject.GetComponent<PlayerControllerCustom>().InspectPressed -= this.Activate;
            playerObject = null;
        }
    }

    

    public void Activate(NetworkIdentity playerId)
    {
        playerObject = playerId.gameObject;
        inspectUI.SetActive(false);
        if (isClue)
        {
            //isClueUI.SetActive(true);
            playerObject.GetComponent<ClueManager>().ClueFound();
        }
        else
        {
            playerObject.GetComponent<ClueManager>().NotClueFound();
            //isNotClueUI.SetActive(true);
        }
        //StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(displayTime);
        Deactivate();
    }

    public void Deactivate()
    {
        if (isClue)
        {
            isClueUI.SetActive(false);
            this.isClue = false;
        }
        else
        {
            isNotClueUI.SetActive(false);
        }
        inspectUI.SetActive(true);
    }
}
