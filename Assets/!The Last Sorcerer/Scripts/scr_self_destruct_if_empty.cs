using UnityEngine;

public class scr_self_destruct_if_empty : MonoBehaviour
{
    public GameObject lifeline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( lifeline == null)
        {
            Destroy(gameObject);
        }
    }
}
