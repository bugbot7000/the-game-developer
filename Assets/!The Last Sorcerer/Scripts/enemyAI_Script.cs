using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    public GameObject sprite;
    public SpriteRenderer spriteRenderer;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange, attackRange;
    public float archerRetreatRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool playerTooCloseToArcher;

    public bool bodyguard = false;
    public GameObject ward;

    public GameObject hitbox;

    public Animator animator;

    private Vector3 rotationSetting;
    private Vector3 velocity;
    private Vector3 previousPosition;

    public float charmPoints;

    public enum EnemyType
    {
        Zombie,
        Ogre,
        Archer
    }
    public EnemyType type;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
        spawnPoint = gameObject.transform.position;
        patrolTarget = spawnPoint;
        if (hitbox != null) // Changed to account for charming objects with no hitbox, may need to revisit later. Hitbox exists for a reason.
        {
            hitbox.SetActive(false);
        }
        //if (type != EnemyType.Zombie) // Commented out until animations are set up
        //{
        //    animator = transform.parent.GetComponent<Animator>();
        //}
        //dmgTxt = GameObject.Find("Dev Log").GetComponent<Text>();
        previousPosition = spawnPoint;
        rotationSetting = new Vector3(0, 0, 0);

        if (sprite != null) { spriteRenderer = sprite.GetComponent<SpriteRenderer>(); }


    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        playerTooCloseToArcher = Physics.CheckSphere(transform.position, archerRetreatRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) { Pursue(); }
        if (playerInSightRange && playerInAttackRange) { Attack(); }
        if (!playerInSightRange && !playerInAttackRange) { Patrol(); }
        

        Vector3 currentPosition = transform.position;
        Vector3 direction = currentPosition - previousPosition;
        direction.Normalize();

        if (direction.x > 0)
        {
            //activeFirePoint = firePointR;
            rotationSetting = new Vector3(0, 0, 0); //In 3D we rotate on the Y, not Z
            if (spriteRenderer != null) { spriteRenderer.flipX = false; }

        }
        else if (direction.x < 0)
        {
            //activeFirePoint = firePointL;
            rotationSetting = new Vector3(0, 180f, 0);
            if (spriteRenderer != null) { spriteRenderer.flipX = true; }
        }

        previousPosition = currentPosition;

        if(charmPoints > 0f)
        {
            charmPoints -= Time.deltaTime;
        }

        if (playerInSightRange && type == EnemyType.Archer && playerTooCloseToArcher) { Retreat(); }

    }

    public void CharmMe()
    {
        whatIsPlayer = LayerMask.GetMask("Enemies");
        bodyguard = true;
        ward = GameObject.Find("player");
        gameObject.layer = 6;
    }

    public void DeCharm()
    {
        whatIsPlayer = LayerMask.GetMask("Player");
        bodyguard = false;
        ward = null;
        gameObject.layer = 8;
    }

    private bool PitCheck() // We may need to rethink this for enemies who can jump
    {
        LayerMask layerMask = LayerMask.GetMask("Default");

        if (Physics.Raycast(gameObject.transform.position, -Vector3.up, 8f, layerMask))
        {
            return false;
        }
        else
        {
            Debug.Log("OVER PIT");
            return true;
        }
    }

    public void StunSelf()
    {
        agent.enabled = false;
        StartCoroutine(RestoreAgentAfterWait());
    }
    public IEnumerator RestoreAgentAfterWait()
    {
        Debug.Log("STARTED COROUTINE");
        //Debug.Log(agent.GetComponent<enemyAI_Script>().agent);


        yield return new WaitForSeconds(3);
        Debug.Log("WAITED");

        LayerMask layerMask = LayerMask.GetMask("Default");

        if (Physics.Raycast(transform.position, Vector3.down, 8f, layerMask))
        {
            Debug.Log("RESTORED AGENT");

            agent.enabled = true;
        }

    }

    private void FixedUpdate()
    {
        transform.eulerAngles = rotationSetting;
    }

    void Patrol() //we need to set this continuously when we summon, so that the summons follow the player. Bool check and Update?
    {
        //Debug.Log("Patrolling");
        if (bodyguard)
        {
            patrolTarget = ward.transform.position;
        }
        if (agent.enabled)
        {
            agent.SetDestination(patrolTarget);
        }
    }
    void Pursue() //We need to change this so it understands how to purseu multiple targets 
    {
        // Debug.Log("Pursuing");
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        player = potentialTargets[0].gameObject.transform;
        //Debug.Log(potentialTargets);

        if (agent.enabled)
        {
            agent.SetDestination(player.position);
        }

    }

    void Retreat()
    {
        Vector3 directionToPlayer = (transform.position - player.position).normalized;
        Vector3 targetPosition = player.position + directionToPlayer * archerRetreatRange;

        Debug.Log(targetPosition);

        agent.SetDestination(targetPosition);
    }
    void Attack()
    {
        //Debug.Log("Attacking");
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }

        if (!alreadyAttacked && type == EnemyType.Zombie) 
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
        if (!alreadyAttacked && type == EnemyType.Ogre)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            //body.constraints = RigidbodyConstraints.FreezePosition;
            body.constraints = RigidbodyConstraints.FreezeRotation;

            hitbox.SetActive(true); //NOTE: In future, we need to find a way to assign hitbox on spawn for the summon to work
            animator.SetBool("isAttacking", true);
            Debug.Log("Attacking");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        //damageOnCollide = true;
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, slamSpd);
    }

    private void ResetAttack()
    {
        if (type == EnemyType.Zombie)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.linearVelocity = Vector3.zero;
            Debug.Log("Resetting attack");
            damageOnCollide = true;
        }
        if(type == EnemyType.Ogre)
        {
            animator.SetBool("isAttacking", false);
            hitbox.SetActive(false);
            Rigidbody body = GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.None;
            body.constraints = RigidbodyConstraints.FreezeRotationX;
            body.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        alreadyAttacked = false;
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

}
