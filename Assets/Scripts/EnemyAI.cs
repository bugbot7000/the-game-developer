using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float attackRange = 0.5f; // Distance at which enemy attacks
    private Transform player;
    public Animator anim;
    private NavMeshAgent agent;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
      
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.stoppingDistance = attackRange; // Stop at attack range
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (agent.enabled)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            anim.Play("Run");
        }
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        anim.Play("Attack");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled = false; // Disable NavMeshAgent to stop movement
        anim.Play("Death");
        GetComponent<Collider>().enabled = false; // Disable collider to avoid further interactions
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(2f); // Wait for death animation to finish
        Destroy(gameObject);
    }
}
