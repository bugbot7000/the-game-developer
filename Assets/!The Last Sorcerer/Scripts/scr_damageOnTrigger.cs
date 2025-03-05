using UnityEngine;

public class scr_damageOnTrigger : MonoBehaviour
{
    public float dmg = 3f;
    public LayerMask WhatIsTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.transform.parent != null && gameObject.GetComponentsInParent<enemyAI_Script>() != null) //Check if we're fatherless
        { 
            if (gameObject.GetComponentInParent<enemyAI_Script>().hitbox == null) { gameObject.GetComponentInParent<enemyAI_Script>().hitbox = gameObject; }
        }
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<scr_health>() != null && ((1 << other.gameObject.layer) & WhatIsTarget) != 0)
        {
            other.gameObject.GetComponent<scr_health>().TakeDamage(dmg);
            //gameObject.SetActive(false);
        }
    }
}
