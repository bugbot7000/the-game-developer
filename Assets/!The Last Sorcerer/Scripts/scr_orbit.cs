using UnityEngine;

public class scr_orbit : MonoBehaviour
{
    public Transform centralObject;  // The object to orbit around
    public float orbitSpeed = 10f;   // Speed of the orbit
    public float orbitDistance = 2f; // Distance from the central object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (centralObject != null)
        {
            // Calculate the new position for the orbiting object
            Vector3 orbitPosition = centralObject.position + (transform.position - centralObject.position).normalized * orbitDistance;

            // Rotate the object around the central object
            transform.RotateAround(centralObject.position, Vector3.up, orbitSpeed * Time.deltaTime);

            Vector3 newPosition = transform.position;
            newPosition.y = centralObject.position.y; // Match the Y-axis of the target object
            transform.position = newPosition; // Apply the new position
        }
    }
}
