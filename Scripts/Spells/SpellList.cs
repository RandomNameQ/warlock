using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour, ISpellsList
{
    [SerializeField]
    private List<GameObject> _spellsPrefab;

    [SerializeField]
    private List<Spells> _spells;
    public List<Spells> Spells
    {
        get { return _spells; }
    }

    public List<Spells> GetSpells()
    {
        return _spells;
    }

    private void Awake()
    {
        foreach (var spellPrefab in _spellsPrefab)
        {
            Spells spell = spellPrefab.GetComponent<Spells>();
            if (spell != null)
            {
                _spells.Add(spell);
            }
        }
    }

    private void Start() { }
}
