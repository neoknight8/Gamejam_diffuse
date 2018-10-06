using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : SingletonMonoBehaviour<PlayersManager>
{

    Player[] players;
    // Use this for initialization
    void Start()
    {
        players = GetComponentsInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Player GetPlayer(int id)
    {
        return players[id - 1];
    }
}
