using PilotoStudio;
using UnityEngine;

public class StupidFuckingBeamStopFinder : MonoBehaviour
{
    public BeamEmitter punkAssBeamScript;
    private void Awake()
    {
        punkAssBeamScript = GetComponent<BeamEmitter>();
        GameObject player = GameObject.Find("player");
        punkAssBeamScript.beamTarget = player.GetComponent<scr_playerController>().currnetBeamTarget.transform;
     
    }

    //private void Update()
    //{
    //    Input.GetKeyUp(KeyCode.J);
    //    { Destroy(gameObject); }
    //    Input.GetKeyUp(KeyCode.I);
    //    { Destroy(gameObject); }
    //}
}
