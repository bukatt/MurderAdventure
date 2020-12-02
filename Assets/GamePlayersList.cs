using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayersList : MonoBehaviour
{
    public List<GamePlayer> gamePlayers = new List<GamePlayer>();

    public void AddPlayer(GamePlayer gp)
    {
        gamePlayers.Add(gp);
    }

    public void RemovePlayer(GamePlayer gp)
    {
        gamePlayers.Remove(gp);
    }
}
