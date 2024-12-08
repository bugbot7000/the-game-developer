using UnityEngine;

public class scr_damageOnTrigger : MonoBehaviour
{
    public float dmg = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<scr_health>() != null)
        {
            other.gameObject.GetComponent<scr_health>().TakeDamage(dmg);
        }
    }
}
