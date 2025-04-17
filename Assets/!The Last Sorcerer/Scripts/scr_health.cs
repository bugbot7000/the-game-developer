using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scr_health : MonoBehaviour
{
    public float health;
    public Text dmgTxt;
    public bool invincible = false;
    public Animator anim;
    public enemyAI_Script enemyAI;

    public delegate void PlayerDamagedHandler();
    public static event PlayerDamagedHandler OnPlayerDamaged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            if (anim != null) { anim.SetBool("DEAD", true); } //Note that this bool needs to be the same for ALL CHARACTERS

            if (gameObject.CompareTag("Player"))
            {
                if (dmgTxt != null)
                {
                    dmgTxt.text = "> RESPAWNING";
                    Invoke(nameof(ResetDevLogText), 0.5f);
                }
                transform.position = GameObject.Find("RESPAWN").transform.position;
                health = 20;
            }
            else
            {
                if (anim == null)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void TakeDamage(float dmgTaken)
    {
        if (invincible) { return; }
        health -= dmgTaken;
        if (anim != null) { anim.SetBool("HURT", true); } //Note that this bool needs to be the same for ALL CHARACTERS
        if (dmgTxt != null)
        {
            dmgTxt.text = "> DEALT " + dmgTaken.ToString() + " DAMAGE";
            Invoke(nameof(ResetDevLogText), 0.5f);
        }
        if (gameObject.CompareTag("Player"))
        {
            invincible = true;
            Invoke(nameof(DisableInvincibility), 0.3f);
            OnPlayerDamaged?.Invoke();
        }
    }

    public void BeUnhurt()
    {
        if (anim != null) { anim.SetBool("HURT", false); } //Note that this bool needs to be the same for ALL CHARACTERS
    }

    public void Die()
    {
        if (enemyAI != null)
        {
            if (enemyAI != null) { gameObject.layer = 0; }
            enemyAI.agent.enabled = false;
            enemyAI.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisableInvincibility() { invincible = false; }
    

    public void ResetDevLogText()
    {
        dmgTxt.text = ">";
    }
}
