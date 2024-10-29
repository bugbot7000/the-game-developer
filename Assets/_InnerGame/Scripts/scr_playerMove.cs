using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class scr_playerMove : MonoBehaviour
{
    public Rigidbody2D body;
    public float mSpd;
    public float defaultSpd = 5f;
    public float dTime = 0.5f;

    Vector2 velocity;
    public GameObject firePointU;
    public GameObject firePointD;
    public GameObject firePointR;
    public GameObject firePointL;
    public GameObject activeFirePoint;
    public GameObject equippedSpell;
    public GameObject spellA;
    public GameObject spellB;
    public GameObject currentAttack;

    public bool embiggenActive;
    public bool quickenActive;
    public bool mirrorActive;

    public Vector3 rotationSetting;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        activeFirePoint = firePointD;
        rotationSetting = new Vector3(0, 0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector2.zero;
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        if (velocity.x > 0) { activeFirePoint = firePointR;
            rotationSetting = new Vector3(0, 0, 90f);
        }
        else if (velocity.x < 0) { activeFirePoint = firePointL;
            rotationSetting = new Vector3(0, 0, -90f);
        }
        else if (velocity.y > 0 && velocity.x == 0) { activeFirePoint = firePointU;
            rotationSetting = new Vector3(0, 0, 180f);
        }
        else if (velocity.y < 0 && velocity.x == 0) { activeFirePoint = firePointD;
            rotationSetting = new Vector3(0, 0, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Y)) { Debug.Log(Input.GetAxisRaw("Vertical")); }
        if (Input.GetKeyDown(KeyCode.X)) { Debug.Log(Input.GetAxisRaw("Horizontal")); }
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            if (equippedSpell == spellA) { equippedSpell = spellB; }
            else if (equippedSpell == spellB) { equippedSpell = spellA; }
        }
        if (Input.GetKeyDown(KeyCode.J)) { Attack(equippedSpell); }

        if (currentAttack != null) { mSpd = 0f; }
        else if (currentAttack == null) {  mSpd = defaultSpd; }

        if (embiggenActive && !quickenActive) { dTime = 2f; }
        else if (quickenActive && !embiggenActive) { dTime = 0.2f; }
        else if (quickenActive && embiggenActive) { dTime = 0.8f; }
        else { dTime = 0.5f; }
    }

    void Attack(GameObject castSpell)
    {
        if (currentAttack == null) 
        { 
            var copy = Instantiate(castSpell, activeFirePoint.transform.position, Quaternion.identity);
            copy.transform.eulerAngles = rotationSetting;
            currentAttack = copy;
            if (embiggenActive)
            {
                Embiggen(copy);
            }
            if (quickenActive) 
            {
                Quicken(copy);
            }
            if (mirrorActive) 
            {
                Mirror(copy); 
            }
            Destroy(copy, dTime );
        }
    }

    void Embiggen (GameObject spell)
    {
        if (spell != null)
        {
            Vector3 spellScale = spell.transform.localScale;
            spell.transform.localScale = new Vector3(spellScale.x + 1f, spellScale.y + 1f, spellScale.z);
        }
    }

    void Quicken (GameObject spell) 
    {  if (spell != null)
        {
            Vector3 spellScale = spell.transform.localScale;
            spell.transform.localScale = new Vector3(spellScale.x - 0.5f, spellScale.y - 0.5f, spellScale.z);
        } 
    }

    void Mirror(GameObject spell)
    {
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        Vector3 rotationReverse = new Vector3(0, 0, 0);
        if (spell != null)
        {
            if (activeFirePoint == firePointD) { spawnPoint = firePointU.transform.position;
                rotationReverse = new Vector3(0, 0, 180f);
            }
            else if (activeFirePoint == firePointU) { spawnPoint = firePointD.transform.position;
                rotationReverse = new Vector3(0, 0, 0f);
            }
            else if (activeFirePoint == firePointR) { spawnPoint = firePointL.transform.position;
                rotationReverse = new Vector3(0, 0, -90f);
            }
            else if (activeFirePoint == firePointL) { spawnPoint = firePointR.transform.position;
                rotationReverse = new Vector3(0, 0, 90f);
            }

            var spellCopy = Instantiate(spell, spawnPoint, Quaternion.identity);
            spellCopy.transform.eulerAngles = rotationReverse;
            Destroy(spellCopy, dTime );
        }
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + velocity * mSpd * Time.fixedDeltaTime);
    }
}
