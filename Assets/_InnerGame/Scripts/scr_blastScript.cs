using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_blastScript : MonoBehaviour
{

    //IEnumerator WaitAndSelfDestruct ()
    //{
    //    // suspend execution for 1 seconds
    //    yield return new WaitForSeconds(1);
        
    //}
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
