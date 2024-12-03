using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyAI_Script : MonoBehaviour
{
    public Text dmgTxt;

    public NavMeshAgent agent;
    public float health, slamSpd, dmg;
    public bool damageOnCollide = false;
    public bool large;
    public Vector3 spawnPoint;
    public Vector3 patrolTarget;
    public LayerMask whatIsPlayer; //Set this on summon to change it to enemies, also change layer to 'familiar layer'
    public Transform player;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool bodyguard = false;
    public GameObject ward;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
        spawnPoint = gameObject.transform.position;
        patrolTarget = spawnPoint;
        //dmgTxt = GameObject.Find("Dev Log").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) { Pursue(); }
        if (playerInSightRange && playerInAttackRange) { Attack(); }
        if (!playerInSightRange && !playerInAttackRange) { Patrol(); }


        //if (health <= 0) { gameObject.SetActive(false); }
        //Debug.Log(agent.destination);
        //if (agent.destination == gameObject.transform.position) { Debug.Log("Reached destination"); }
    }

    void Patrol() //we need to set this continuously when we summon, so that the summons follow the player. Bool check and Update?
    {
        //Debug.Log("Patrolling");
        if (bodyguard)
        {
            patrolTarget = ward.transform.position;
        }
        agent.SetDestination(patrolTarget);
    }
    void Pursue() //We need to change this so it understands how to purseu multiple targets 
    {
        // Debug.Log("Pursuing");
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        player = potentialTargets[0].gameObject.transform;
        Debug.Log(potentialTargets);

        agent.SetDestination(player.position);
    }
    void Attack()
    {
        //Debug.Log("Attacking");
        agent.SetDestination(transform.position);

        if (!alreadyAttacked) 
        {
            damageOnCollide = true;
            //transform.LookAt(player);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, slamSpd);

            Rigidbody body = GetComponent<Rigidbody>();

            // Get direction from your postion toward the object you wish to push
            var direction =  player.position - body.transform.position;
            //Debug.Log(direction);

            //Normalize keeps the value of a vector, but reduces it to 1. We use this to determine the direction of the pushed object relative to the player
            body.AddForce(direction.normalized * slamSpd, ForceMode.Impulse);
            //body.AddForce(transform.forward * slamSpd, ForceMode.Impulse);
            Debug.Log("Attacking");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        //damageOnCollide = true;
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, slamSpd);
    }

    private void ResetAttack()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.linearVelocity = Vector3.zero;
        Debug.Log("Resetting attack");
        alreadyAttacked = false;
        damageOnCollide = true;
    }

    //private void PauseAttack()
    //{
    //    Rigidbody body = GetComponent<Rigidbody>();
    //    body.linearVelocity = Vector3.zero;
    //}

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

    // Old attack
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && damageOnCollide == true)
    //    {
    //        collision.gameObject.GetComponent<scr_playerController>().health -= dmg;
    //        damageOnCollide = false;
    //        if (dmgTxt != null) 
    //        {
    //            dmgTxt.text = "> RECIEVED " + dmg.ToString() + " DAMAGE";
    //            Invoke(nameof(ResetDevLogText), 0.5f);
    //        }

    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<scr_health>() != null && damageOnCollide == true)
        {
            collision.gameObject.GetComponent<scr_health>().TakeDamage(dmg);
            damageOnCollide = false;
            if (dmgTxt != null)
            {
                dmgTxt.text = "> RECIEVED " + dmg.ToString() + " DAMAGE";
                Invoke(nameof(ResetDevLogText), 0.5f);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    //Old trigger collider system//
    //private void OnTriggerStay(Collider other) //We use stay instead of enter because Stay updates the position consistently
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        agent.SetDestination(other.transform.position);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        agent.SetDestination(spawnPoint);
    //    }
    //}
}
