using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scr_health : MonoBehaviour
{
    public float health;
    public Text dmgTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) { 
            if (gameObject.CompareTag("Player"))
            {
                dmgTxt.text = "> RESPAWNING";
                Invoke(nameof(ResetDevLogText), 0.5f);    
                transform.position = GameObject.Find("RESPAWN").transform.position;
                health = 20;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float dmgTaken)
    {
        health -= dmgTaken;
        if (dmgTxt != null)
        {
            dmgTxt.text = "> DEALT " + dmgTaken.ToString() + " DAMAGE";
            Invoke(nameof(ResetDevLogText), 0.5f);
        }
    }

    public void ResetDevLogText()
    {
        dmgTxt.text = ">";
    }
}
