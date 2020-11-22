using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
public class PlayerDeath : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnDead))]
    public bool isDead = false;

    public GameObject deadPlayer;
    public event Action<bool> playerKilled;
    public GameObject alivePlayer;

    public void OnDead(bool oldDead, bool newDead)
    {
        isDead = newDead;
        if (newDead)
        {
            playerKilled?.Invoke(newDead);
            //Instantiate(deadPlayer, transform.position, transform.rotation);
            deadPlayer.SetActive(true);
            alivePlayer.SetActive(false);
            //Instantiate(bloodSplatter, transform.position, transform.rotation);
            //mySpriteRenderer.sprite = deadSprite;
        }

        foreach(Collider2D c in gameObject.GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
    }

}
