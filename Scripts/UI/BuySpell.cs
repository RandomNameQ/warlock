using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySpell : MonoBehaviour
{
    public Spells SpellToBuy;
    private SpellMediator _spellMediator;
    private void Awake() {
        _spellMediator = FindAnyObjectByType<SpellMediator>();
    }

    public void Buy()
    {
        if (SpellToBuy ==null)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        
        CheckForGoldAndANother();
        _spellMediator.UpdatePlayerSkill(SpellToBuy);
        transform.parent.gameObject.SetActive(false);
        SpellToBuy=null;
    }

  

    private void CheckForGoldAndANother() { }
}
