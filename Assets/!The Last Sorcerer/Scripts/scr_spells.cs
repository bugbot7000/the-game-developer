using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_spells : MonoBehaviour
{
    public scr_dialogueScript dmgTxt;



    //This script is intended to work on an object that comes into existence briefly and then goes away
    public float pushForce, pushRadius, pushDamage, slashDamage;
    public GameObject pulled;
    private Vector3 pullVelocity = Vector3.zero;

    public GameObject player, wand;

    //The idea is that we have a bool for each spell.
    //We write them as functions and then use the bools to turn them on and off.
    //Hitboxes are handled by the object we attach this script to
    public bool PushSpell, PullSpell, CharmSpell, SlashSpell, FreezeSpell;
    // Start is called before the first frame update
    void Start()
    {
        // Finds the first game object with the 'player' tag.
        // Fairly certain this only works so long as there is only 1 object with said tag. But we're single player anyways
        player = GameObject.FindGameObjectWithTag("Player");
        wand = GameObject.FindGameObjectWithTag("Wand");
    }

    private void FixedUpdate()
    {
        if (PullSpell && pulled != null) 
        {
            //pulled.transform.position = Vector3.SmoothDamp(pulled.transform.position, transform.position, ref pullVelocity, 0.3f);
            pulled.transform.position = Vector3.MoveTowards(pulled.transform.position, wand.transform.position, 1f);
        }
    }

    //We look for targets as soon as we touch something
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other != null && other.gameObject.CompareTag("Target") ||
            other != null && other.gameObject.CompareTag("Enemy")) 
        {
            if (PushSpell) { Push(other.gameObject); }
            else if (PullSpell) { Pull(other.gameObject); }
            else if (CharmSpell) { Charm(other.gameObject); }
            else if (SlashSpell) {  Slash(other.gameObject); }
            else if (FreezeSpell) { Freeze(other.gameObject); }

            //if (other.gameObject.CompareTag("Enemy")) { other.gameObject.GetComponent<enemyAI_Script>().health -= pushDamage; }
        }

    }

    public void Push(GameObject pushedObject)
    {
        //Debug.Log(pushedObject);
        Rigidbody pushedBody = pushedObject.GetComponent<Rigidbody>();

        // Get direction from your postion toward the object you wish to push
        var direction = pushedBody.transform.position - player.transform.position;
        Debug.Log(direction);

        //Normalize keeps the value of a vector, but reduces it to 1.
        //We use this to determine the direction of the pushed object relative to the player
        //pushedBody.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
        if (pushedObject.GetComponent<enemyAI_Script>() != null)
        {
            if (pushedBody.GetComponent<enemyAI_Script>().large == false)
            {
                pushedBody.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
            }
            else if (pushedBody.GetComponent<enemyAI_Script>().large == true)
            {
                pushedObject.GetComponent<enemyAI_Script>().StunSelf();

                pushedBody.AddForce(direction.normalized * pushForce * 0.1f, ForceMode.Impulse);
            }
        }
        else { pushedBody.AddForce(direction.normalized * pushForce, ForceMode.Impulse); }
        //if (pushedObject.CompareTag("Enemy")) 
        //{
        //    //pushedObject.GetComponent<enemyAI_Script>().health -= pushDamage;
        //    pushedObject.GetComponent<enemyAI_Script>().TakeDamage(pushDamage);
        //    //TriggerDialogue();
        //    //Invoke(nameof(ExitDialogue), 0.5f);
        //}

        if (pushedObject.GetComponent<scr_health>() != null)
        {
            pushedObject.GetComponent<scr_health>().TakeDamage(pushDamage);
        }
        pushedObject = null; //Probably don't need this

    }

    public void Pull(GameObject pulledObject)
    {
        if (pulledObject.GetComponent<enemyAI_Script>() == null || pulledObject.GetComponent<enemyAI_Script>().large == false)
        {
            if (pulled == null)
            {
                pulled = pulledObject;
                if (pulled.GetComponent<enemyAI_Script>() != null)
                {
                    pulled.GetComponent<enemyAI_Script>().enabled = false;
                    pulled.GetComponent<NavMeshAgent>().enabled = false;

                }
            }
        }
        else if (pulledObject.GetComponent<enemyAI_Script>().large == true)
        {
            pulledObject.GetComponent <enemyAI_Script>().StunSelf();
            Rigidbody pulledBody = pulledObject.GetComponent<Rigidbody>();

            // Get direction from your postion toward the object you wish to push
            var direction = pulledBody.transform.position - player.transform.position;

            //Normalize keeps the value of a vector, but reduces it to 1.
            //We use this to determine the direction of the pushed object relative to the player
            pulledBody.AddForce(-direction.normalized * pushForce *0.5f, ForceMode.Impulse);
            //StartCoroutine(pulledObject.GetComponent<enemyAI_Script>().RestoreAgentAfterWait());
        }

    }

    public void Charm(GameObject charmedObject)
    {
        scr_playerController playerScript = player.GetComponent<scr_playerController>();
        if (charmedObject.GetComponent<enemyAI_Script>() != null && charmedObject.GetComponent<scr_health>() != null)
        {
            enemyAI_Script AI = charmedObject.GetComponent<enemyAI_Script>();
            scr_health hpScript = charmedObject.GetComponent<scr_health>();
            AI.charmPoints += 2f;
            if (hpScript.health > AI.charmPoints) { return; }
        }
        if ( playerScript.charmedThrall != null) 
        {
            if (playerScript.charmedThrall.CompareTag("Enemy")) { playerScript.charmedThrall.GetComponent<enemyAI_Script>().DeCharm(); }
            else if (playerScript.charmedThrall.CompareTag("Target"))
            {
                Destroy(playerScript.charmedThrall.GetComponent<enemyAI_Script>());
                Destroy(playerScript.charmedThrall.GetComponent<NavMeshAgent>());
                Destroy(playerScript.charmedThrall.GetComponent<scr_health>());

            }
        }
        playerScript.charmedThrall = charmedObject;
        {
            
        }
        if (charmedObject.GetComponent<NavMeshAgent>() == null)
        {
            charmedObject.AddComponent<NavMeshAgent>();
            //Debug.Log("NavMesh added");
        }
        if (charmedObject.GetComponent<scr_health>() == null)
        {
            charmedObject.AddComponent<scr_health>();
            charmedObject.GetComponent<scr_health>().health = 4;
            //Debug.Log("NavMesh added");
        }
        if (charmedObject.GetComponent<Rigidbody>() != null) 
        {
            Rigidbody body = charmedObject.GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezeRotationX;
            body.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        if (charmedObject.GetComponent<enemyAI_Script>() != null)
        {
            charmedObject.GetComponent<enemyAI_Script>().CharmMe();
        }
        else
        {
            charmedObject.AddComponent<enemyAI_Script>();
            enemyAI_Script charmedObjectAIScript = charmedObject.GetComponent<enemyAI_Script>();
            charmedObjectAIScript.CharmMe();
            charmedObjectAIScript.type = enemyAI_Script.EnemyType.Zombie;
            charmedObjectAIScript.slamSpd = 15f;
            charmedObjectAIScript.dmg = 2f;
            charmedObjectAIScript.sightRange = 10f;
            charmedObjectAIScript.attackRange = 5f;
        }

    }

    public void Slash (GameObject slashedObject)
    {
        if (slashedObject.GetComponent<scr_health>() != null)
        {
            slashedObject.GetComponent<scr_health>().TakeDamage(slashDamage);
        }
    }

    public void Freeze (GameObject frozenObject) //This function is in scr_playerController because it needs to use IEnumerator to control timing
    {
        player.GetComponent<scr_playerController>().Freeze(frozenObject);
    }

    //IEnumerator RestoreAgent(GameObject agent)// Doesn't work because the spell is destroyed before the routine finishes
    //{
    //    Debug.Log("STARTED COROUTINE");
    //    //Debug.Log(agent.GetComponent<enemyAI_Script>().agent);


    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("WAITED");

    //    LayerMask layerMask = LayerMask.GetMask("Default");

    //    if (Physics.Raycast(agent.transform.position, -Vector3.up, 8f, layerMask))
    //    {
    //        Debug.Log("RESTORED AGENT");

    //        agent.GetComponent<enemyAI_Script>().agent.enabled = true;
    //    }

    //}

    //public void RestorreAgent(GameObject agent)
    //{
    //    LayerMask layerMask = LayerMask.GetMask("Default");

    //    if (Physics.Raycast(agent.transform.position, -Vector3.up, 8f, layerMask))
    //    {
    //        agent.GetComponent<enemyAI_Script>().agent.enabled = true;
    //    }
    //}

    public void TriggerDialogue()
    {
        //FindObjectOfType<scr_dialogueManager>().StartDialogue(dmgTxt);
        Object.FindFirstObjectByType<scr_dialogueManager>().StartDialogue(dmgTxt);
    }

    public void ExitDialogue()
    {
        //FindObjectOfType<scr_dialogueManager>().EndDialogue();
        Object.FindFirstObjectByType<scr_dialogueManager>().EndDialogue();

    }

    private void OnDestroy()
    {
        if (pulled != null)
        {
            if (pulled.GetComponent<enemyAI_Script>() != null)
            {
                pulled.GetComponent<enemyAI_Script>().enabled = true;
                pulled.GetComponent<NavMeshAgent>().enabled = true;

            }
        }
    }
}
