using UnityEngine;

public class scr_Arrow : MonoBehaviour
{

    public float speed = 20f;
    public float lifetime = 10f;
    public float damage = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Handle collision with the player
        if (other.CompareTag("Player")) //enemy layer used to go here but the enemies were shooting themselves, will figure out other method for charmed/summoned enemies later
        {
            other.gameObject.GetComponent<scr_health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
