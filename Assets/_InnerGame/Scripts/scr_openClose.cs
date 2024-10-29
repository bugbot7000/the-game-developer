using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_openClose : MonoBehaviour
{
    public bool open, wallTouch, gateTouch; //Bools for whether or not we're touching wall, other gate, and whether or not we're open
    public float move, d_move; //move is speed and direction when we're open, d_move is the same for when we're clsoed
    public GameObject gateWall, otherGate; //Objects for wall and other side of gate

    private void FixedUpdate()
    {
        if (open && !wallTouch) { transform.Translate(0, move, 0); } //If we're open, but not touching a wall, we move towards the wall until we do
        else if (!open && !gateTouch) { transform.Translate(0, d_move, 0); } //If we're closed and not touching the other gate, we move towards it until we do
    }

    //Check if we're touching the wall or the other gate
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject == gateWall)
        {
            wallTouch = true;
        }
        else if (collision.gameObject == otherGate) 
        {
            gateTouch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == gateWall)
        {
            wallTouch = false;
        }
        else if (collision.gameObject == otherGate)
        {
            gateTouch = false;
        }
    }
}
