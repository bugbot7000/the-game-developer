using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_openGate : MonoBehaviour
{
    public GameObject gateDoorA, gateDoorB, gateTrigger; //Stores the gates we need to open as objects, as well as the object that triggers them
    public scr_openClose gateAScript, gateBScript; //Handle for the opening and closing scripts on the gates
    // Start is called before the first frame update
    void Start()
    {
        gateAScript = gateDoorA.GetComponent<scr_openClose>();
        gateBScript = gateDoorB.GetComponent<scr_openClose>(); //Grab the scripts from attached objects
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == gateTrigger) { gateAScript.open = true; gateBScript.open = true; } //Sets gates to be open when object touches trigger...
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == gateTrigger) { gateAScript.open = false; gateBScript.open = false; } //...And to close when we aren't touching it
    }
}
