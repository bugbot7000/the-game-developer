using UnityEngine;

public class New_DM_Orbit_scr : MonoBehaviour
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

            float angle = Time.time * orbitSpeed;
            var positionCenterObject = centralObject.position;

            var x = positionCenterObject.x + Mathf.Cos(angle) * orbitOffset;
            var z = positionCenterObject.z + Mathf.Sin(angle) * orbitOffset;
            transform.position = new Vector3(x, centralObject.transform.position.y + 3.5f, z);
        }
    }
}
