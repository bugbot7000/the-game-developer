using UnityEngine;

public class ArenaAudienceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    void Start()
    {
        int rand = Random.Range(0, 3);
        if(transform.GetChild(rand)!=null)
        transform.GetChild(rand).gameObject.SetActive(true);
    }

    
}
