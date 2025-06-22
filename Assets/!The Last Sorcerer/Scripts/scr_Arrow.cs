using UnityEngine;

public class scr_Arrow : MonoBehaviour
{

    float speed = 20f;
    float lifetime = 10f;
    float damage = 1f;
    public bool DMAtk;
    // add reference to enemy ai scrip, assing when instantiaded
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Handle collision with the player
        if (other.CompareTag("Player")) // change to look for the target of ai script
        {
            other.gameObject.GetComponent<scr_health>().TakeDamage(damage);
            if (DMAtk) { GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.rock_crash); }
            else
            {
                GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.blunt);
            }
            Destroy(gameObject);
        }
    }
}
