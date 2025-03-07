using UnityEngine;

public class DeveloperNote : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}