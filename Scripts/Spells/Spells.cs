using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class Spells : NetworkBehaviour
{
    [SerializeField]
    private string _spellName;
    public string SpellName
    {
        get { return _spellName; }
        set { _spellName = value; }
    }

    [SerializeField]
    private Sprite _spriteSpell;

    public Sprite SpriteSpell
    {
        get { return _spriteSpell; }
        set { _spriteSpell = value; }
    }

    public int Price { get; set; } = 10;
    public float Damage { get; set; } = 1;
    public float Range { get; set; } = 5;
    public float Cooldown { get; set; } = 10;
    public string Description { get; set; } = "bla";

    [SerializeField]
    public bool IsProjectile { get; set; } = true;

    [SerializeField]
    private readonly float _lifeTime = 10f;

    [SerializeField]
    private SpellPool _spellPoolScript;
    private Rigidbody rb;

    [SerializeField]
    private float _range;

    [SerializeField]
    private bool canCast;

    [SerializeField]
    private bool _isActivatedFirstClick;

    [SerializeField]
    private float _speedFly = 1f;

    private float _timer;

    [SerializeField]
    private GameObject _currentSpellPrefab;

    [SerializeField]
    List<GameObject> projectiles = new List<GameObject>();

    private void Awake()
    {
        Initialization();
        // TODO фиксани эту хуйню
        if (!gameObject.name.Contains("Ball"))
        {
            IsProjectile = false;
            return;
        }

        _currentSpellPrefab = gameObject;
        rb = GetComponent<Rigidbody>();

        _spellPoolScript = FindAnyObjectByType<SpellPool>();

        rb.isKinematic = false;
    }

    private void Start() { }

    private void Update()
    {
        if (IsProjectile)
        {
          
            // fix
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
            }
            Vector3 movement = transform.forward * _speedFly;
            rb.velocity = movement;
            DespawnFromTime();
        }
    }

    private GameObject GetPoolObject()
    {
        for (var i = 0; i < _spellPoolScript.gameObject.transform.childCount; i++)
        {
            var child = _spellPoolScript.gameObject.transform.GetChild(i).gameObject;

            if (!child.activeSelf && child.name == this.GetType().ToString())
            {
                return child;
            }
        }
        return GenerateNewProj();
    }

    private GameObject GenerateNewProj()
    {
        var proj = Instantiate(_currentSpellPrefab, transform.position, Quaternion.identity);
        return proj;
    }

    
    [ServerRpc(RequireOwnership = false)]
    virtual public void Cast(Transform playerTranform)
    {
        /* var spell1 = GameObject.Find("Pool").GetComponent<SpellPool>()._spellPrefabs[0]; */
        
        var spell = NetworkManager.GetPooledInstantiated(gameObject, 0, false);
        spell = Instantiate(spell, playerTranform.position, Quaternion.identity);
        spell.transform.position = playerTranform.position;
        spell.transform.localRotation = playerTranform.rotation;
        
       /*  spell.SetActive(true); */
      
        base.Spawn(spell,Owner);
        
    }

    public int GiveOwner(){
        return base.OwnerId;
    }

    virtual public void Fly() { }

    virtual public void DespawnFromTime()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime && gameObject.activeSelf)
        {
            _timer = 0;
            gameObject.SetActive(false);
        }
    }

    virtual public void DespawnFromRange() { }

    virtual public void DespawnFromHit() { }

    virtual public void Initialization()
    {
        Price = 100;
        Damage = 1;
        Range = 3;
        Cooldown = 10;
        SpellName = this.GetType().ToString();
        Description = this.GetType().ToString()+" bla";
       
        
    }
}
