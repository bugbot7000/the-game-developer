using UnityEngine;

public class scr_playerSprite : MonoBehaviour
{
    scr_playerController player;
    Animator animator;
    bool flipX;

    void Start()
    {
        player = FindFirstObjectByType<scr_playerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = player.transform.position;        
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("speed", 
            new Vector3(horizontal, 0, vertical).magnitude
        );

        if (horizontal < 0 && !flipX) 
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            flipX = true;
        }
        else if (horizontal > 0 && flipX)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.J) && !animator.GetCurrentAnimatorStateInfo(0).IsName("SoldierAttack"))
        {
            animator.SetTrigger("attack");
        }
    }
}