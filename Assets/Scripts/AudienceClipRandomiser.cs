using UnityEngine;

public class AudienceClipRandomiser : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateIndex()
    {
        anim.SetFloat("ActionIndex",Random.Range(0,8));
    }
}
