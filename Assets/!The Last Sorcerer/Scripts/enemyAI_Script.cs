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

    public GameObject hitbox;

    public Animator animator;

    private Vector3 rotationSetting;
    private Vector3 velocity;
    private Vector3 previousPosition;

    public enum EnemyType
    {
        Zombie,
        Ogre
    }
    public EnemyType type;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
        spawnPoint = gameObject.transform.position;
        patrolTarget = spawnPoint;
        hitbox.SetActive(false);
        animator = GetComponent<Animator>();
        //dmgTxt = GameObject.Find("Dev Log").GetComponent<Text>();
        previousPosition = spawnPoint;
        rotationSetting = new Vector3(0, 0, 0);
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

        //Vector3 currentPosition = transform.position;
        //Vector3 direction = currentPosition - previousPosition;
        //direction.Normalize();

        //if (direction.x > 0)
        //{
        //    //activeFirePoint = firePointR;
        //    rotationSetting = new Vector3(0, 90f, 0); //In 3D we rotate on the Y, not Z
        //}
        //else if (direction.x < 0)
        //{
        //    //activeFirePoint = firePointL;
        //    rotationSetting = new Vector3(0, -90f, 0);
        //}
        //else if (direction.z > 0 && direction.x == 0)
        //{
        //    //activeFirePoint = firePointU;
        //    rotationSetting = new Vector3(0, 0, 0);
        //}
        //else if (direction.z < 0 && direction.x == 0)
        //{
        //    //activeFirePoint = firePointD;
        //    rotationSetting = new Vector3(0, 180f, 0f);
        //}

        //previousPosition = currentPosition;

        Vector3 direction;


        if (playerInSightRange && playerInAttackRange) 
        { 
            direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            if (angle > 45 && angle <= 135)
            {
                //Face up
                //spriteRenderer.flipX = false; // Hypothetical sprite code
                rotationSetting = new Vector3(0, 180, 0);
            }
            else if(angle > -135 && angle <= -45)
            {
                //Face down
                rotationSetting = new Vector3(0, 0, 0);
            }
            else if (angle > -45 && angle <= 45)
            {
                //Face right
                rotationSetting = new Vector3(0, -90, 0);
            }
            else
            {
                //Face left
                //spriteRenderer.flipX = true; 
                rotationSetting = new Vector3(0, 90, 0);
            }
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
            body.constraints = RigidbodyConstraints.FreezePosition;
            hitbox.SetActive(true);
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
