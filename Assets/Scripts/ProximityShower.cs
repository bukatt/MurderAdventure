using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityShower : MonoBehaviour
{
    public GameObject uiToShow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.player)
        {
            if (collision.gameObject.GetComponent<GamePlayer>().isLocalPlayer)
            {
                uiToShow.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.player)
        {
            if (collision.gameObject.GetComponent<GamePlayer>().isLocalPlayer)
            {
                uiToShow.SetActive(false);
            }
        }
    }
}
