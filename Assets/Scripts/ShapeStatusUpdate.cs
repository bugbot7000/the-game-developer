using System;
using UnityEngine;

public class ShapeStatusUpdate : MonoBehaviour
{
    public Animator anim;
    public AbilitySelector Abs;

    scr_playerController player;
    private void Start()
    {
        player = FindObjectOfType<scr_playerController>();
    }
    
    private void Update()
    {
        SwitchShape();
    }

    void SwitchShape()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (Abs.currentShape == AbilitySelector.Shapes.Swipe)
                {
                    anim.Play("Beam");
                    Abs.currentShape = AbilitySelector.Shapes.Beam;
                    player.SwitchSpell1ToBeam();
                }
                else if (Abs.currentShape == AbilitySelector.Shapes.Beam)
                {
                    anim.Play("Shield");
                    Abs.currentShape = AbilitySelector.Shapes.Shield;
                    player.SwitchSpell1ToShield();
                }
                else if (Abs.currentShape == AbilitySelector.Shapes.Shield)
                {
                    anim.Play("Swipe");
                    Abs.currentShape = AbilitySelector.Shapes.Swipe;
                    player.SwitchSpell1ToSwipe();
                }
            }
        }
    }
}
