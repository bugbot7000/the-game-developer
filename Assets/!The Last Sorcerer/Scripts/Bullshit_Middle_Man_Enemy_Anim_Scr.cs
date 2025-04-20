using UnityEngine;

public class Bullshit_Middle_Man_Enemy_Anim_Scr : MonoBehaviour
{
    public GameObject actualFuckingEnemy;
    public enemyAI_Script actualFuckingScript;
    public scr_health fuckingHealthScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualFuckingScript = actualFuckingEnemy.GetComponent<enemyAI_Script>();
        fuckingHealthScript = actualFuckingEnemy.GetComponent<scr_health>();
    }

    public void AssVanish() { actualFuckingScript.AssassinVanish(); }
    public void UnHurtYourself() { fuckingHealthScript.BeUnhurt(); }

    // Update is called once per frame
    void Update()
    {
        
    }
}
