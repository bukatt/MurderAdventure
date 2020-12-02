using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class GameUIManager : NetworkBehaviour
{
    private static GameUIManager _instance;

    public GameObject murdererWins;

    public GameObject innocentWins;

    public GameObject gameOverHostUI;

    public GameObject tieUI;

    public KeyCode menuOpenKey;

    public void ShowMurdererWins()
    {
        murdererWins.SetActive(true);
        if (isServer)
        {
            gameOverHostUI.SetActive(true);
        }
    }

    public void ShowInnocentWins()
    {
        innocentWins.SetActive(true);
        if (isServer)
        {
            gameOverHostUI.SetActive(true);
        }
    }

    public void ShowTie()
    {
        tieUI.SetActive(true);
        if (isServer)
        {
            gameOverHostUI.SetActive(true);
        }
    }

    private void Update()
    {
        
    }
}
