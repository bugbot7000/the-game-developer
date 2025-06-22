using UnityEngine;

using System.Collections;

public class SetSpellToPush : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;
        
        GetComponent<scr_playerController>().ChangeFirstSpellEffect(0);
    }
}