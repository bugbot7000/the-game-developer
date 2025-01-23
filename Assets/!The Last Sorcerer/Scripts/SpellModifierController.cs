using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SpellModifierController : MonoBehaviour
{
    public enum Spells
    {
        Spell1,
        Spell2
    }
    public enum Shapes
    {
        Beam,
        Swipe
    }
    public enum Effects
    {
        Push,
        Pull
    }
    //UI References
    public Animator ActiveSpellAnim,SelectionAnim;
    public Spells activeSpell = Spells.Spell1;
    [Header("Spell 1")]
    public Shapes spellOneActiveShape = Shapes.Beam;
    public Effects spellOneActiveEffect = Effects.Pull;
    public GameObject SpellOneShapeList;
    public GameObject SpellOneEffectList;
    [Header("Spell 2")]
    public Shapes spellTwoActiveShape = Shapes.Beam;
    public Effects spellTwoActiveEffect = Effects.Pull;
    public GameObject SpellTwoShapeList;
    public GameObject SpellTwoEffectList;
    
    

    public void ToggleSpell()
    {   
        
        if (activeSpell == Spells.Spell1)
        {
            activeSpell = Spells.Spell2;
            ActiveSpellAnim.Play("SwitchToSpell2");
            SelectionAnim.Play("GoToSpell2");
        }
        else
        {
            activeSpell = Spells.Spell1;
            ActiveSpellAnim.Play("SwitchToSpell1");
            SelectionAnim.Play("GoToSpell1");
        }
    }

    public void ToggleShapeList(int spellIndex)
    {
        if (spellIndex == 0)
            SpellOneShapeList.gameObject.SetActive(!SpellOneShapeList.gameObject.activeSelf);
        else
            SpellTwoShapeList.gameObject.SetActive(!SpellTwoShapeList.gameObject.activeSelf);
    }
    public void ToggleEffectList(int spellIndex)
    {
        if(spellIndex == 0)
        SpellOneEffectList.gameObject.SetActive(!SpellOneEffectList.gameObject.activeSelf);
        else
            SpellTwoEffectList.gameObject.SetActive(!SpellTwoEffectList.gameObject.activeSelf);
    }

    public void OnSpellOneShapeToggleValueChanged(int shape)
    {
        switch (shape)
        {
            case 0:
                spellOneActiveShape = Shapes.Beam;

                break;
            case 1:
                spellOneActiveShape = Shapes.Swipe;
                break;
        }
    }
    public void OnSpellOneEffectToggleValueChanged(int effect)
    {
        switch (effect)
        {
            case 0:
                spellOneActiveEffect = Effects.Push;
                break;
            case 1:
                spellOneActiveEffect = Effects.Pull;
                break;
        }
    }
    
    public void OnSpellTwoShapeToggleValueChanged(int shape)
    {
        switch (shape)
        {
            case 0:
                spellTwoActiveShape = Shapes.Beam;

                break;
            case 1:
                spellTwoActiveShape = Shapes.Swipe;
                break;
        }
    }
    public void OnSpellTwoEffectToggleValueChanged(int effect)
    {
        switch (effect)
        {
            case 0:
                spellTwoActiveEffect = Effects.Push;
                break;
            case 1:
                spellTwoActiveEffect = Effects.Pull;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Spell: "+activeSpell + " Shape: " + spellOneActiveShape + " Effect : " + spellOneActiveEffect);
        }
    }
}
