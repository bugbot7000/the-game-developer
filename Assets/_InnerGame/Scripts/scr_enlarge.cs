using UnityEngine;

public class scr_enlarge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //If the beam touches something...
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Wall")) //We check to make sure it's eligable for the spell
        {
            Debug.Log(collision);
            Vector3 objScale = collision.transform.localScale;
            if (objScale.x < 7f && objScale.y < 7f) { collision.gameObject.transform.localScale = new Vector3(objScale.x + objScale.x, objScale.y + objScale.y, objScale.z); }
            //And channge the size if it's within the acceptable size range
        }
    }
}
