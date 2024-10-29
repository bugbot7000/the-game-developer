using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_reduce : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //If the beam touches something...
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Wall")) //We check to make sure it's eligable for the spell
        {
            Debug.Log(collision);
            Vector3 objScale = collision.transform.localScale; //Check it's current size...
            if (objScale.x > 0.25f && objScale.y > 0.25f) { collision.gameObject.transform.localScale = new Vector3(objScale.x - objScale.x/2, objScale.y - objScale.y/2, objScale.z); }
            //And channge the size if it's within the acceptable size range
        }
    }
}
