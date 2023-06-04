using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class FullFillShop : NetworkBehaviour
{
    [SerializeField]
    private List<GameObject> _shopSLots = new List<GameObject>();

    [SerializeField]
    private GameObject spellList;

    [SerializeField]
    private List<Spells> _spells;

    public override void OnStartClient()
    {
        base.OnStartClient();
        spellList = GameObject.Find("List");

        if (spellList.TryGetComponent<ISpellsList>(out var spellsList))
        {
            _spells = spellsList.GetSpells();
        }
        UpdateSkillShopUI();
        if (!base.IsOwner)
            return;
    }

    private void UpdateSkillShopUI()
    {
        for (var i = 0; i < _spells.Count; i++)
        {
            var uiSprite= _shopSLots[i].GetComponent<Image>().sprite = _spells[i].SpriteSpell;
            var spellType = _spells[i].GetType();
            _shopSLots[i].AddComponent(spellType);
            
            var c =_shopSLots[i].GetComponent<Spells>().SpriteSpell = uiSprite;
            
        }
    }
}
