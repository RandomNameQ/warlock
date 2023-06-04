using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class PlayerConnectionData : NetworkBehaviour
{
    [SerializeField]
    public List<NetworkConnection> PlayerId;

    public override void OnStartServer()
    {
        base.OnStartServer();
        PlayerId = new List<NetworkConnection>();
        

        
    }

    public void AddPlayer(NetworkConnection playerID)
    {
        PlayerId.Add(playerID);

        foreach (var playerId in PlayerId)
        {
            Debug.Log(playerId.ClientId +" player connect");
        }
    }

    public void RemovePlayer(NetworkConnection playerID)
    {
        PlayerId.Remove(playerID);
    }
}
