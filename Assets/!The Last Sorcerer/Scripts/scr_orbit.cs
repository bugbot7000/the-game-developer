using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class scr_orbit : MonoBehaviour
{
    public Transform centralObject;  // The object to orbit around
    public float orbitSpeed = 10f;   // Speed of the orbit
    //public float orbitDistance = 2f; // Distance from the central object
    public float angle = 0f;
    public float orbitOffset = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (centralObject != null)
        {
            //// Calculate the new position for the orbiting object
            //Vector3 orbitPosition = centralObject.position + (transform.position - centralObject.position).normalized * orbitDistance;

            //// Rotate the object around the central object
            //transform.RotateAround(centralObject.position, Vector3.up, orbitSpeed * Time.deltaTime);

            //Vector3 newPosition = transform.position;
            // Calculate the new position of the orbiting object


            //float angle = orbitSpeed * Time.deltaTime;
            //Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * orbitDistance;
            //transform.position = centralObject.position + offset;
            //transform.RotateAround(centralObject.position, Vector3.up, orbitSpeed * Time.deltaTime);

            // Increment the angle
            //angle += orbitSpeed * Time.deltaTime;

            //// Calculate the new position
            //float x = Mathf.Cos(angle) * orbitDistance;
            //float z = Mathf.Sin(angle) * orbitDistance;

            //// Set the new position
            //transform.position = new Vector3(x, transform.position.y, z) + centralObject.position;

            //// Look at the target to maintain facing direction
            //transform.LookAt(centralObject);
            //transform.position = new Vector3(transform.position.x, centralObject.transform.position.y, transform.position.z);

            float angle = Time.time * orbitSpeed;
            var positionCenterObject = centralObject.position;

            var x = positionCenterObject.x + Mathf.Cos(angle) * orbitOffset;
            var z = positionCenterObject.z + Mathf.Sin(angle) * orbitOffset;
            transform.position = new Vector3(x, centralObject.transform.position.y, z);

            //newPosition.y = centralObject.position.y; // Match the Y-axis of the target object
            //transform.position = newPosition; // Apply the new position
        }
    }
}
