using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class SpellMediator : NetworkBehaviour
{
    [SerializeField]
    private PlayerSkills _playerSkills;

    public GameObject cube;

    public void UpdatePlayerSkill(Spells spell)
    {
        _playerSkills.AddSkillToPlayer(spell);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Instantiate(cube);
        }
        _playerSkills = FindAnyObjectByType<PlayerSkills>();
       /*  Debug.Log(base.OwnerId + " server");

        if (base.IsServer) {Debug.Log("server"); }
        if (base.IsClient) {Debug.Log("client"); } */
    }

    private void Start() { }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Instantiate(cube);
          
        }
    }
}
