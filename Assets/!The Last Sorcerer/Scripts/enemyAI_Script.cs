using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class enemyAI_Script : MonoBehaviour
{
    public Text dmgTxt;

    public Animator anim;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public float health, slamSpd, dmg;
    public bool damageOnCollide = false;
    public bool large, noMove = false;
    public Vector3 spawnPoint;
    public Vector3 patrolTarget;
    public LayerMask whatIsPlayer; //Set this on summon to change it to enemies, also change layer to 'familiar layer'
    public Transform player;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange, attackRange;
    public float retreatRange, retreatDistance;
    public bool playerInSightRange, playerInAttackRange;
    public bool playerTooClose;

    public bool bodyguard = false;
    public GameObject ward;

    public GameObject hitbox, nose;
    public GameObject mesh;

    public Animator animator;

    private Vector3 rotationSetting;
    private Vector3 velocity;
    private Vector3 previousPosition;
    private Vector3 currentPositionCheck;
    private Vector3 previousPositionCheck;
    public float moveAnimThreshold = 1f;

    public float charmPoints;

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    public bool visible = true;
    public bool stalking = true;

    public Transform[] DMLeapLocations;
    public float DMJumpSpeed;
    private int DMCurrentJumpIndex = 0;
    public bool DMIsJumping = false;
    public Transform[] projectileSpawnPoints;
    private int projectileSpawnIndex = 0;

    public float spawnInterval = 3f;
    public GameObject spawnedEnemy;
    public Transform enemySpawnPoint;

    public enum EnemyType
    {
        Zombie,
        Ogre,
        Archer,
        Assassin,
        Knight,
        DM,
        Necromancer,
        Sprite
    }
    public EnemyType type;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
        spawnPoint = gameObject.transform.position;
        patrolTarget = spawnPoint;
        rb = gameObject.GetComponent<Rigidbody>();
        if (hitbox != null && type != EnemyType.Ogre) // Changed to account for charming objects with no hitbox, may need to revisit later. Hitbox exists for a reason.
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

        if (type == EnemyType.Necromancer) { StartCoroutine(SpawnEnemies()); }
        if (type == EnemyType.Assassin) { visible = false; }
        currentPositionCheck = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        playerTooClose = Physics.CheckSphere(transform.position, retreatRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange && type != EnemyType.Sprite) { Pursue(); }
        if (playerInSightRange && playerInAttackRange && type != EnemyType.Sprite) { Attack(); }
        if (!playerInSightRange && !playerInAttackRange && type != EnemyType.Sprite) { Patrol(); }
        if (type == EnemyType.Sprite && !playerInSightRange) { Patrol(); }
        

        Vector3 currentPosition = transform.position;
        Vector3 direction = currentPosition - previousPosition;
        direction.Normalize();

        //if (direction.x > 0 && type != EnemyType.Knight)
        //{
        //    rotationSetting = new Vector3(0, 90, 0); //In 3D we rotate on the Y, not Z
        //    if (spriteRenderer != null) { spriteRenderer.flipX = false; }

        //}
        //else if (direction.x < 0 && type != EnemyType.Knight)
        //{
        //    rotationSetting = new Vector3(0, -90f, 0);
        //    if (spriteRenderer != null) { spriteRenderer.flipX = true; }
        //}
        //else if (direction.z > 0 && direction.x == 0 && type != EnemyType.Knight)
        //{
        //    rotationSetting = new Vector3(0, 0, 0);
        //}
        //else if (direction.z < 0 && direction.x == 0 && type != EnemyType.Knight)
        //{
        //    rotationSetting = new Vector3(0, 180f, 0f);
        //}

        previousPosition = currentPosition;
        


        if (charmPoints > 0f)
        {
            charmPoints -= Time.deltaTime;
        }

        if (playerInSightRange && type == EnemyType.Archer && playerTooClose) { Retreat(); }
        if (playerInSightRange && type == EnemyType.Necromancer && playerTooClose) { Retreat(); }
        if (playerInSightRange && type == EnemyType.Sprite) { Retreat(); }
        if (type == EnemyType.DM)
        {
            if (playerTooClose && !DMIsJumping) 
            {
                // Start the jump coroutine
                StartCoroutine(JumpToLocation(DMLeapLocations[DMCurrentJumpIndex]));

                // Update the current jump index to the next location
                DMCurrentJumpIndex = (DMCurrentJumpIndex + 1) % DMLeapLocations.Length;
            }
        }
        if (noMove) { agent.enabled = false; }

        if (type == EnemyType.Assassin) 
        {
            if (!visible) { GetComponent<MeshRenderer>().enabled = false; }
            else { GetComponent<MeshRenderer>().enabled = true; }
            if (playerInSightRange && !stalking) { Retreat(); }
            //if (alreadyAttacked) { Retreat(); }
        }

        //if (type == EnemyType.Knight && !alreadyAttacked)
        //{
        //    Vector3 plyerDirection = (player.position - gameObject.transform.position).normalized;
        //    gameObject.transform.rotation = Quaternion.LookRotation(plyerDirection);
        //}

        currentPositionCheck = transform.position;
        float distance = Vector3.Distance(currentPositionCheck, previousPositionCheck);
        float speed = distance / Time.deltaTime;


        if (anim != null) 
        {
            //if (direction.x > 0.1f || direction.y > 0.1f || direction.x < 0.1f || direction.y < 0.1f) // A small threshold to account for tiny movements
            if(speed > moveAnimThreshold)
            {
                anim.SetBool("MOVE", true);
                //Debug.Log("MOVING");
            }
            else
            {
                anim.SetBool("MOVE", false);
            }
        }
        previousPositionCheck = currentPositionCheck;
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

    // Sprite orbit logic
    public void SpriteOrbitStart(GameObject orbit)
    {
        scr_orbit OrbitScript = GetComponent<scr_orbit>();
        OrbitScript.centralObject = orbit.transform;
        //gameObject.transform.SetParent(orbit.transform);
        agent.enabled = false;
    }
    public void SpriteOrbitStop()
    {
        if (GetComponent<scr_orbit>() != null)
        {
            scr_orbit OrbitScript = GetComponent<scr_orbit>();
            transform.position = spawnPoint;
            //if (OrbitScript.centralObject.parent != null)
            //{
            //    gameObject.transform.SetParent(OrbitScript.centralObject.parent);
            //}
            //else
            //{
            //    gameObject.transform.SetParent(null);
            //}
            OrbitScript.centralObject = null;
            agent.enabled = true;
        }
    }

    private void OnEnable()
    {
        if (type == EnemyType.Sprite)
        {
            scr_health.OnPlayerDamaged += SpriteOrbitStop;
        }
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
        //if (type != EnemyType.Knight) //Using the knight as a test case for getting rid of old rotation logic
        //{
        //    transform.eulerAngles = rotationSetting;
        //}
        if (!alreadyAttacked && player != null)
        {
            Vector3 plyerDirection = (player.position - gameObject.transform.position).normalized;
            gameObject.transform.rotation = Quaternion.LookRotation(plyerDirection);
        }
        if (mesh != null)
        {
            mesh.transform.rotation = gameObject.transform.rotation;
        }
    }

    void Patrol() //we need to set this continuously when we summon, so that the summons follow the player. Bool check and Update?
    {
        //if (type == EnemyType.Sprite) { Debug.Log("Patrolling"); }
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
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            Vector3 directionToPlayer = (transform.position - player.position).normalized;
            Vector3 targetPosition = player.position + directionToPlayer * retreatDistance;

            // Check if the enemy is too close or too far from the player
            if (distanceToPlayer < retreatDistance - 0.5f || distanceToPlayer > retreatDistance + 0.5f)
            {
                if (agent.enabled)
                {
                    agent.SetDestination(targetPosition);
                }
            }
        }
    }
    void Attack()
    {
        //Debug.Log("Attacking");
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }

        if (!alreadyAttacked && player != null) 
        {
            Vector3 plyerDirection = (player.position - gameObject.transform.position).normalized;
            gameObject.transform.rotation = Quaternion.LookRotation(plyerDirection);
        }

        if (!alreadyAttacked && type == EnemyType.Zombie) 
        {
            damageOnCollide = true;
            //transform.LookAt(player);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, slamSpd);

            Rigidbody body = GetComponent<Rigidbody>();

            // Get direction from your postion toward the object you wish to push
            if(player != null)
            {
                var direction = player.position - body.transform.position;
                body.AddForce(direction.normalized * slamSpd, ForceMode.Impulse);
            }
            //Debug.Log(direction);

            //Normalize keeps the value of a vector, but reduces it to 1. We use this to determine the direction of the pushed object relative to the player
            //body.AddForce(transform.forward * slamSpd, ForceMode.Impulse);
            //Debug.Log("Attacking");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        if (!alreadyAttacked && type == EnemyType.Ogre)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            //body.constraints = RigidbodyConstraints.FreezePosition;
            body.constraints = RigidbodyConstraints.FreezeRotation;

            //hitbox.GetComponent<BoxCollider>().enabled = true; //NOTE: In future, we need to find a way to assign hitbox on spawn for the summon to work
            //animator.SetBool("isAttacking", true); //CODE FOR STARTING ANIMATION GOES HERE. ANIMATION NEEDS TO NOT LOOP(!) FOR THIS CODE TO WORK
            animator.SetTrigger("ATK1 Trg");
            Debug.Log("Attacking");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        if (!alreadyAttacked && type == EnemyType.Archer && !playerTooClose) 
        {
            if (arrowPrefab != null && arrowSpawnPoint != null) 
            {
                Vector3 direction = (player.position - arrowSpawnPoint.position).normalized;
                gameObject.transform.rotation = Quaternion.LookRotation(direction);

                GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
                arrow.transform.rotation = Quaternion.LookRotation(direction);
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        if (type == EnemyType.DM)
        {
            if (!alreadyAttacked)
            {
                if (arrowPrefab != null && projectileSpawnPoints != null)
                {
                    Vector3 direction = (player.position - projectileSpawnPoints[projectileSpawnIndex].position).normalized;
                    gameObject.transform.rotation = Quaternion.LookRotation(direction);

                    GameObject projectile = Instantiate(arrowPrefab, projectileSpawnPoints[projectileSpawnIndex].position, Quaternion.identity);
                    projectile.transform.rotation = Quaternion.LookRotation(direction);
                    projectileSpawnIndex = (projectileSpawnIndex + 1) % projectileSpawnPoints.Length;
                }
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        if (!alreadyAttacked && type == EnemyType.Necromancer && !playerTooClose)
        {
            if (arrowPrefab != null && arrowSpawnPoint != null)
            {
                NecromanerBlastPlayAnim();
            }
        }

        if (type == EnemyType.Assassin) 
        {
            //Vector3 direction = (player.position - gameObject.transform.position).normalized;
            //gameObject.transform.rotation = Quaternion.LookRotation(direction);
            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                Rigidbody body = GetComponent<Rigidbody>();
                body.constraints = RigidbodyConstraints.FreezePosition;
                //agent.enabled = false;
                //agent.SetDestination(transform.position);
                agent.speed = 0;
                Invoke("AssassinAppear", 0.5f);
            }
        }

        if (type == EnemyType.Knight && !alreadyAttacked)
        {
            alreadyAttacked = true;
            Rigidbody body = GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezePosition;
            body.constraints = RigidbodyConstraints.FreezeRotation;
            agent.enabled = false;

            Invoke("KnightAttack", 0.5f);
        }
        //damageOnCollide = true;
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, slamSpd);
    }

    public void AttackGap()
    {
        anim.SetBool("ATTACK", false);
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    public void NecromancerBlast()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - arrowSpawnPoint.position).normalized;
            gameObject.transform.rotation = Quaternion.LookRotation(direction);

            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
            arrow.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void NecromanerBlastPlayAnim()
    {
        anim.SetBool("ATTACK", true);
    }

    private void ResetAttack()
    {
        if (type == EnemyType.Zombie)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.linearVelocity = Vector3.zero;
            //Debug.Log("Resetting attack");
            damageOnCollide = true;
        }
        if (type == EnemyType.Ogre)
        {
            //animator.SetBool("isAttacking", false); //CODE FOR ANIMATION TO END. AGAIN, WE ARE NOT LOOPING. THE "timeBetweenAttacks" VARIABLE DETERMINES ATTACK TIMING
            //hitbox.GetComponent<BoxCollider>().enabled = false;
            Rigidbody body = GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.None;
            body.constraints = RigidbodyConstraints.FreezeRotationX;
            body.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        if (type == EnemyType.Assassin) { stalking = true; }
        if (type == EnemyType.Knight)
        {
            hitbox.SetActive(false);
            Rigidbody body = GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.None;
            body.constraints = RigidbodyConstraints.FreezeRotationX;
            body.constraints = RigidbodyConstraints.FreezeRotationZ;
            agent.enabled = true;
        }
        alreadyAttacked = false;
    }

    public void EnableBossHitbox()
    {
        hitbox.GetComponent<BoxCollider>().enabled = true;
    }
    public void DisableBossHitbox()
    {
        hitbox.GetComponent<BoxCollider>().enabled = false;
    }

    public void ToggleVisibility() 
    {
        if (visible) { visible = false; } 
        else { visible = true; }
    }

    public void AssassinAppear()
    {
        //ToggleVisibility();
        Invoke("AssassinAttack", 0.5f);
    }

    public void AssassinAttack()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezePosition;
        //body.constraints = RigidbodyConstraints.FreezeRotation;

        //hitbox.SetActive(true); //NOTE: In future, we need to find a way to assign hitbox on spawn for the summon to work
        //animator.SetBool("isAttacking", true);

        anim.SetTrigger("ATTACK");
        //Invoke("AssassinVanish", 3.5f);

        // start the animation
        // place the vanish invocation  inside the animation
    }

    public void AssassinVanish()
    {
        stalking = false;
        //hitbox.SetActive(false);
        Rigidbody body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.None;
        body.constraints = RigidbodyConstraints.FreezeRotationX;
        body.constraints = RigidbodyConstraints.FreezeRotationZ;
        //ToggleVisibility();
        //agent.enabled = true;
        agent.speed = 16;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    public void KnightAttack()
    {
        hitbox.SetActive(true); //NOTE: In future, we need to find a way to assign hitbox on spawn for the summon to work
                                //animator.SetBool("isAttacking", true);
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
    private IEnumerator JumpToLocation(Transform jumpLocation)
    {
        DMIsJumping = true;
        //agent.enabled = false;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        while (elapsedTime < DMJumpSpeed)
        {
            transform.position = Vector3.Lerp(startingPosition, jumpLocation.position, (elapsedTime / DMJumpSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = jumpLocation.position;
        //agent.enabled = true;
        DMIsJumping = false;
    }
    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnInterval);

        if (spawnedEnemy != null && playerInAttackRange && enemySpawnPoint != null)
        {
            anim.SetBool("CALL", true);
        }
        StartCoroutine(SpawnEnemies());
    }

    public void MakeZombie()
    {
        var enemy = Instantiate(spawnedEnemy, enemySpawnPoint.position, transform.rotation);
        anim.SetBool("CALL", false);
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
