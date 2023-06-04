using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet;

public class PlayerSkills : NetworkBehaviour
{
    private List<Spells> _playerSkills = new List<Spells>();
    private ControlMovementToUseSpell _controlMovementToUseSpell;

    public GameObject prefabSkill;

    [SerializeField]
    private bool _canShoot;

    [SerializeField]
    private bool _hotKeySpellPressed;
    private bool _secondClickActivated;

    private void Awake()
    {
        _controlMovementToUseSpell = GetComponent<ControlMovementToUseSpell>();
       
    }


    private void Update()
    {
        _canShoot = _controlMovementToUseSpell.CanShoot();
        PressButtons();
        if (_canShoot && _secondClickActivated)
        {
            GeneratePro(base.Owner, transform);
            _secondClickActivated = false;
            _controlMovementToUseSpell.ClarMoveState(true);
        }
    }

    [ServerRpc]
    void GeneratePro(NetworkConnection conn, Transform playerTransform)
    {
        NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(prefabSkill, true);
        nob.transform.position = playerTransform.position;
        nob.transform.rotation = playerTransform.rotation;
        InstanceFinder.NetworkManager.ServerManager.Spawn(nob, conn);
    }

    private void PressButtons()
    {
        if (Input.GetKeyDown(KeyCode.Q) || _hotKeySpellPressed)
        {
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            playerMovement.CanShootInPoint = false;

            _hotKeySpellPressed = true;
            if (Input.GetMouseButtonDown(0))
            {
                _controlMovementToUseSpell.ClarMoveState(true);
                _secondClickActivated = true;
                _controlMovementToUseSpell.IsClick = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _secondClickActivated = false;
            _hotKeySpellPressed = false;
            _controlMovementToUseSpell.IsClick = false;
        }
    }

    public void AddSkillToPlayer(Spells skill)
    {
        if (!_playerSkills.Contains(skill))
        {
            _playerSkills.Add(skill);
            var spellComponent = gameObject.AddComponent(skill.GetType()) as Spells;
            spellComponent.IsProjectile = false;
        }
        else
        {
            Debug.Log("Skill already exists in the player's skill list.");
        }
    }

    public void RemoveSkillFromPlayer(Spells skill)
    {
        if (_playerSkills.Contains(skill))
        {
            _playerSkills.Remove(skill);
        }
        else
        {
            Debug.Log("Skill does not exist in the player's skill list.");
        }
    }
}
