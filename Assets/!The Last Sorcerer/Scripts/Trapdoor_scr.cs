using System.Collections;
using UnityEngine;

public class Trapdoor_scr : MonoBehaviour
{
    public float delayBeforeDisappear = 2f; // Time player needs to stand before platform disappears
    public float respawnTime = 5f; // Time before the platform reappears
    private bool isTriggered = false; // Ensures the platform only reacts once at a time

    public MeshRenderer platformRenderer; // For visibility
    public BoxCollider platformCollider; // For collision

    private void Start()
    {
        platformRenderer = GetComponent<MeshRenderer>();
        platformCollider = GetComponent<BoxCollider>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !isTriggered)
    //    {
    //        isTriggered = true; // Ensure it's only triggered once
    //        StartCoroutine(DisappearAfterDelay());
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true; // Ensure it's only triggered once
            StartCoroutine(DisappearAfterDelay());
        }
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDisappear);
        Debug.Log("Platform disappeared!");

        // Disable platform
        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        // Wait for respawn time
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("Platform reappeared!");

        // Re-enable platform
        platformRenderer.enabled = true;
        platformCollider.enabled = true;

        isTriggered = false; // Reset for future triggers
    }
}
