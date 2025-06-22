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
    public GameObject HealthBarFill;
    public GameObject HealthBarFlash;

    public delegate void PlayerDamagedHandler();
    public static event PlayerDamagedHandler OnPlayerDamaged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAI = GetComponent<enemyAI_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            if (anim != null) { anim.SetBool("DEAD", true); } //Note that this bool needs to be the same for ALL CHARACTERS

            if (gameObject.CompareTag("Player"))
            {
                //anim.SetBool("DEAD", true) ;
                if (dmgTxt != null)
                {
                    dmgTxt.text = "> RESPAWNING";
                    Invoke(nameof(ResetDevLogText), 0.5f);
                }
                Invoke(nameof(RespawnPlayer), 4f);
            }
            else
            {
                if (anim == null)
                {
                    Destroy(gameObject);
                }
                else { Die(); }
            }
        }
    }

    public void TakeDamage(float dmgTaken)
    {
        if (invincible) { return; }
        health -= dmgTaken;
        Debug.Log("Taken DMG");
        if (anim != null && !gameObject.CompareTag("Player")) 
        {
            anim.SetTrigger("HURT");
            Debug.Log("AM HURT");
            GetComponent<Rigidbody>().freezeRotation = true;
            if(HealthBarFill != null) { HealthBarFill.GetComponent<HealthBar>().DidTakeDamage(dmgTaken); } //FIXME
            if (HealthBarFlash != null) { HealthBarFlash.GetComponent<HealthBar>().DidTakeDamage(dmgTaken); } //FIXME
        } 
        if (dmgTxt != null)
        {
            dmgTxt.text = "> DEALT " + dmgTaken.ToString() + " DAMAGE";
            Invoke(nameof(ResetDevLogText), 0.5f);
        }
        if (gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("HURT");
            invincible = true;
            Invoke(nameof(DisableInvincibility), 0.6f);
            OnPlayerDamaged?.Invoke();
        }
    }

    public void RespawnPlayer()
    {
        anim.SetBool("DEAD", false);
        transform.position = GameObject.Find("RESPAWN").transform.position;
        health = 20;
        //if (HealthBarFill != null) { HealthBarFill.GetComponent<HealthBar>(); } // We need a way to refill the healthbar when we respawn, but I saw no such method.
    }

    public void BeUnhurt()
    {
        if (anim != null) { anim.SetBool("HURT", false); } //Note that this bool needs to be the same for ALL CHARACTERS
        GetComponent<Rigidbody>().freezeRotation = false;
    }

    public void Die()
    {
        if (enemyAI != null)
        {
            Debug.Log("Enemy death");
            gameObject.layer = 0;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(GetComponent<enemyAI_Script>());
            //enemyAI.agent.enabled = false;
            GetComponent<enemyAI_Script>().enabled = false;
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponent<scr_health>());
            //enemyAI.enabled = false;
        }
        else
        {
            Debug.Log("Destruction");
            Destroy(gameObject);
        }
    }
    public void PlayMinotaurDeathFX()
    {
        GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.minotaur_death);
    }

    public void DisableInvincibility() { invincible = false; }
    

    public void ResetDevLogText()
    {
        dmgTxt.text = ">";
    }
}
