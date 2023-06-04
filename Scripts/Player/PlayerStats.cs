using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using TMPro;

public class PlayerStats : NetworkBehaviour, ITakeDamage
{
    private int health = 10;
    public string _playerName = "test";

    public GameObject test;

    public override void OnStartClient()
    {
        base.OnStartClient();
        Invoke("ChangeName", 0.1f);
        if (!base.IsOwner)
        {
            GetComponent<PlayerStats>().enabled = false;
        }
        else { }
    }

   /*  void ChangeName()
    {
        WhoIsOwner.DisplayComponents(this);
        ComponentDebugger.DisplayComponents(gameObject);

        test = GameObject.Find("Test");

        if (test == null)
        {
            Despawn(gameObject);
            return; // Exit the method if 'test' is null
        }

        int clientId = GetComponent<PlayerMPData>().PlayerId.ClientId;
        if (clientId >= 0 && clientId < test.transform.childCount)
        {
            TextMeshProUGUI textMeshPro = test.transform
                .GetChild(clientId)
                .GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = "player " + clientId;
            }
        }
    } */

    private void Start() { }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Despawn(gameObject);
            /* base.Despawn(); //networkBehaviour. Despawns the NetworkObject. */
            /* networkObject.Despawn(); //referencing a NetworkObject.
            InstanceFinder.ServerManager.Despawn(gameObject); //through ServerManager. */
            Die1();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(11);
        }
    }

    [ServerRpc]
    private void Die()
    {
        gameObject.SetActive(false);
    }

    [ObserversRpc]
    private void Die1()
    {
        /* gameObject.SetActive(false); */
    }
}
