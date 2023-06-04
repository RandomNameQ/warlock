using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class testpool : NetworkBehaviour
{
    [SyncVar]
    public NetworkObject prefab;

    [SyncVar]
    NetworkObject instans;

    NetworkConnection newOwnerConnection;

    PlayerConnectionData playerConnectionData;

    List<NetworkObject> list = new List<NetworkObject>();

    public override void OnStartServer()
    {
        base.OnStartServer();
        /* SpawnObject(); */
        

        
    }

    public void SpawnObject()
    {
        for (var i = 0; i < 10; i++)
        {
            instans = Instantiate(prefab);
            list.Add(instans);
            Spawn(instans);
           
        }
         UpdateName();
        
    }

    [ObserversRpc(ExcludeOwner = true)]
    public void UpdateName()
    {
         Debug.Log("RpcPlayFootstepAudio");
        for (var i = 0; i < list.Count; i++)
        {
            
            list[i].name = "qweasd";
        }
    }
}
