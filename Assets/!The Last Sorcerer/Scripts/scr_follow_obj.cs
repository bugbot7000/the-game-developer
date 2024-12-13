using UnityEngine;

public class scr_follow_obj : MonoBehaviour
{
    public GameObject follow_obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow_obj != null)
        {
            transform.position = follow_obj.transform.position;
        }
    }
}
