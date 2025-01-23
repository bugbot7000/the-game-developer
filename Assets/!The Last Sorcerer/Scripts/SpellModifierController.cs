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
    public Animator ActiveSpellAnim;
    public Spells activeSpell = Spells.Spell1;
    public Shapes activeShape = Shapes.Beam;
    public Effects activeEffect = Effects.Pull;
    
    [FormerlySerializedAs("SpellList")] public GameObject ShapeList;
    public GameObject EffectList;

    public void ToggleSpell()
    {   
        
        if (activeSpell == Spells.Spell1)
        {
            activeSpell = Spells.Spell2;
            ActiveSpellAnim.Play("SwitchToSpell2");
        }
        else
        {
            activeSpell = Spells.Spell1;
            ActiveSpellAnim.Play("SwitchToSpell1");
        }
    }

    public void ToggleShapeList()
    {
        ShapeList.gameObject.SetActive(!ShapeList.gameObject.activeSelf);
    }
    public void ToggleEffectList()
    {
        EffectList.gameObject.SetActive(!EffectList.gameObject.activeSelf);
    }

    public void OnShapeToggleValueChanged(int shape)
    {
        switch (shape)
        {
            case 0:
                activeShape = Shapes.Beam;

                break;
            case 1:
                activeShape = Shapes.Swipe;
                break;
        }
    }
    public void OnEffectToggleValueChanged(int effect)
    {
        switch (effect)
        {
            case 0:
                activeEffect = Effects.Push;
                break;
            case 1:
                activeEffect = Effects.Pull;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Spell: "+activeSpell + " Shape: " + activeShape + " Effect : " + activeEffect);
        }
    }
}
