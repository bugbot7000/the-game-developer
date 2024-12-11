using UnityEngine;

public class scr_mageModelAnimator : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("speed", 
            new Vector3(horizontal, 0, vertical).magnitude
        );        
    }
}