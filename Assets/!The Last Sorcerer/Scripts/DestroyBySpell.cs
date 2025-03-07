using UnityEngine;

public class DestroyBySpell : MonoBehaviour
{
    [SerializeField] string targetSpell;
    
    void OnTriggerEnter(Collider other)
    {
        scr_spells spells = other.GetComponent<scr_spells>();

        if (spells != null && other.gameObject.name.Contains(targetSpell))
        {
            Destroy(gameObject);
        }
    }
}