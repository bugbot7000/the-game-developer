using UnityEngine;

public class EnemySprite : MonoBehaviour
{
    [SerializeField] Transform enemyTransform;

    Animator animator;
    Vector3 previousPosition;
    bool flipX;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyTransform == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = enemyTransform.position;
            
            Vector3 speed = (transform.position - previousPosition) / Time.deltaTime;
            
            animator.SetFloat("speed", speed.magnitude);

            previousPosition = transform.position;

            if (speed.x < -0.1f && !flipX) 
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                flipX = true;
            }
            else if (speed.x > 0.1f && flipX)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                flipX = false;
            }            
        }
    }
}