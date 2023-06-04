using System.Runtime.InteropServices.ComTypes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using FishNet.Object;
using FishNet.Connection;

public class SpellPool : NetworkBehaviour
{
    [SerializeField]
    private int _countSpellsToPool = 10;

    [SerializeField]
    public List<GameObject> _spellPrefabs = new List<GameObject>();

    public List<GameObject> Projectiles = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsServer)
        {
            return;
        }

        ShowPollToAllClients();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnObject()
    {
        for (var i = 0; i < _countSpellsToPool; i++)
        {
            foreach (var spell in _spellPrefabs)
            {
                var pooledSpell = Instantiate(spell, transform.position, Quaternion.identity);
                pooledSpell.name = spell.name;
                pooledSpell.transform.SetParent(transform);
                Projectiles.Add(pooledSpell);
                /* base.Spawn(pooledSpell); */
                ServerManager.Spawn(pooledSpell);
                pooledSpell.SetActive(false);
                
            }
        }
    /*    ShowPollToAllClients(); */
    }

   [ObserversRpc]
    private void ShowPollToAllClients()
    {
        for (var i = 0; i < _countSpellsToPool; i++)
        {
            foreach (var spell in _spellPrefabs)
            {
                var pooledSpell = Instantiate(spell, transform.position, Quaternion.identity);
                pooledSpell.name = spell.name;
                pooledSpell.transform.SetParent(transform);
                Projectiles.Add(pooledSpell);
                /* base.Spawn(pooledSpell); */
                ServerManager.Spawn(pooledSpell);
                pooledSpell.SetActive(false);
               
            }
        }
    }
}
