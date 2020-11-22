using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour
{
    public GameObject toEnable;

    private bool hovered = false;

    private bool close = false;

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.player)
        {
            if (collision.gameObject.GetComponent<GamePlayer>().isLocalPlayer)
            {
                close = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.player)
        {
            if (collision.gameObject.GetComponent<GamePlayer>().isLocalPlayer)
            {
                close = false;
            }
        }
    }

    private void Update()
    {
        if(close && hovered && !toEnable.activeInHierarchy)
        {
            toEnable.SetActive(true);
        } else if (toEnable.activeInHierarchy && (!close || !hovered))
        {
            toEnable.SetActive(false);
        }
    }
}
