using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class InitializationOrder : NetworkBehaviour
{
    private int _order;

    private void Awake()
    {
        TextOrder("Awake");
    }

    private void Start()
    {
        TextOrder("Start");
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        TextOrder("OnStartServer");
    }

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        TextOrder("OnStartNetwork");
        if (base.Owner.IsLocalClient)
            TextOrder("IsLocalClient");
    }

    public override void OnOwnershipServer(NetworkConnection prevOwner)
    {
        base.OnOwnershipServer(prevOwner);
        TextOrder("OnOwnershipServer");
    }

    public override void OnSpawnServer(NetworkConnection connection)
    {
        base.OnSpawnServer(connection);
        TextOrder("OnSpawnServer");
    }

    public override void OnDespawnServer(NetworkConnection connection)
    {
        base.OnDespawnServer(connection);
        TextOrder("OnDespawnServer");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        TextOrder("OnStopServer");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        TextOrder("OnStartClient");
    }

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);
        TextOrder("OnOwnershipClient");
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        TextOrder("OnStopClient");
    }

    public override void OnStopNetwork()
    {
        base.OnStopNetwork();
        TextOrder("OnStopNetwork");
    }

    private void TextOrder(string method)
    {
        _order++;
        Debug.Log(method + " is " + _order);
    }
}
