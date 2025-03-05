using UnityEngine;

public class KnightShield_scr : MonoBehaviour
{
    public GameObject shielded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DamageFromFront(GameObject incomingDamageSource)
    {
        // Calculate the direction from the enemy to the source of the damage
        Vector3 selfToDamageSource = incomingDamageSource.transform.position - transform.position;

        // Normalize the vectors
        selfToDamageSource.Normalize();
        Vector3 forward = transform.forward;

        // Check if the damage direction is in front of the enemy
        float dotProduct = Vector3.Dot(selfToDamageSource, forward);
        if (dotProduct < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell")) 
        {
            if (DamageFromFront(other.gameObject))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spell")) 
        {
            if (DamageFromFront(other.gameObject))
            {
                shielded.GetComponent<scr_health>().invincible = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spell")) { shielded.GetComponent<scr_health>().invincible = false; }
    }
}
