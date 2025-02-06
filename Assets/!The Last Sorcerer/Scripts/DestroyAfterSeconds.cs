using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float seconds;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}