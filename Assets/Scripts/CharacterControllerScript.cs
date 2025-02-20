using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public Animator anim;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotation from physics
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor for a better FPS-style experience
    }

    void Update()
    {
        Rotate();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * moveZ + transform.right * moveX;
        move.Normalize(); // Prevents diagonal speed boost

        if (move.magnitude > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Move using Rigidbody instead of setting velocity directly
        rb.MovePosition(rb.position + move * speed * Time.deltaTime);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);
    }

    void Attack()
    {
        anim.SetTrigger("canAttack");
    }
}