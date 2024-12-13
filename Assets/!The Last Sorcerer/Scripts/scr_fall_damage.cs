using UnityEngine;

public class scr_fall_damage : MonoBehaviour
{
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.GetComponent<scr_health>() != false)
        {
            collision.gameObject.GetComponent<scr_health>().TakeDamage(damage);
        }
    }
}
