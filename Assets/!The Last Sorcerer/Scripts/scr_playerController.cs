using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class scr_playerController : MonoBehaviour
{

    Vector3 velocity;
    public Rigidbody body;
    public float mSpd;
    public float dashSpd;
    public float dashCooldown;
    public float defaultSpd;
    public float dTime = 0.5f;
    public float health;
    public bool dashing;
    public Vector3 rotationSetting;

    //public GameObject firePointU;
    //public GameObject firePointD;
    //public GameObject firePointR;
    //public GameObject firePointL;
    public GameObject activeFirePoint;
    public GameObject equippedSpell;
    public GameObject equippedFamiliar;
    //public GameObject spellA;
    //public GameObject spellB;
    public GameObject currentAttack;
    public GameObject[] currentFamiliars;
    public bool spellOnCooldown;
    public float cooldownTime;

    public GameObject familiar1, familiar2, familiar3;
    public bool openFamiliarSlot = true;
    // Start is called before the first frame update
    void Start()
    {
        //activeFirePoint = firePointU;
        body = GetComponent<Rigidbody>();
        rotationSetting = new Vector3(0, 0, 0f);
    }



    // Update is called once per frame
    void Update()
    {
        velocity = Vector3.zero;
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.z = Input.GetAxisRaw("Vertical"); //We move on X and Z, not Y. Result of moving from 2 to 3 dimensions
        if (Input.GetKeyDown(KeyCode.Space) && currentAttack == null) { Dash(); }

        if (velocity.x > 0)
        {
            //activeFirePoint = firePointR;
            rotationSetting = new Vector3(0, 90f, 0); //In 3D we rotate on the Y, not Z
        }
        else if (velocity.x < 0)
        {
            //activeFirePoint = firePointL;
            rotationSetting = new Vector3(0, -90f, 0);
        }
        else if (velocity.z > 0 && velocity.x == 0)
        {
            //activeFirePoint = firePointU;
            rotationSetting = new Vector3(0, 0, 0);
        }
        else if (velocity.z < 0 && velocity.x == 0)
        {
            //activeFirePoint = firePointD;
            rotationSetting = new Vector3(0, 180f, 0f);
        }
    


        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    if (equippedSpell == spellA) { equippedSpell = spellB; }
        //    else if (equippedSpell == spellB) { equippedSpell = spellA; }
        //}
        if (Input.GetKeyDown(KeyCode.J) && !dashing) { Attack(equippedSpell); }
        if (Input.GetKeyDown(KeyCode.L) && !dashing) { Summon(equippedFamiliar); }

        if (currentAttack != null)
        {
            if (currentAttack.GetComponent<scr_spells>().PushSpell) { mSpd = 0f; }
            else if (currentAttack.GetComponent<scr_spells>().PullSpell) { mSpd = defaultSpd; }
        }
        else if (currentAttack == null) { mSpd = defaultSpd; }

        if (currentAttack != null)
        {
            if (currentAttack.GetComponent<scr_spells>().PullSpell)
            {
                if (Input.GetKeyUp(KeyCode.J))
                {
                    Destroy(currentAttack.gameObject);
                    currentAttack = null;
                }
            }
        }
        if (health <= 0f) { Death(); }
    }

    public void SlotMyFamiliar(GameObject familiar)
    {

        
            if (familiar1 == null)
            {
                familiar1 = familiar;
                return;
            }
            else if (familiar2 == null)
            {
                familiar2 = familiar;
                return;
            }
            else if (familiar3 == null)
            {
                familiar3 = familiar;
                return;
            }
            else
            {
                return;
            }
        

    }

    public bool CheckForOpenFamiliarSlots()
    {

            if (familiar1 == null || familiar2 == null || familiar3 == null)
            {
                return true;
            }
            else
            {
                return false;
            }

    }

    public void DestroyFamiliar()
    {
        if (familiar1 != null)
        {
            Destroy(familiar1);
            familiar1 = null;
            return;
        }
        else if (familiar2 != null)
        {
            Destroy(familiar2);
            familiar2 = null;
            return;
        }
        else if (familiar3 != null)
        {
            Destroy(familiar1);
            familiar3 =null;
            return;
        }
        else
        {
            return;
        }
    }

    void Attack(GameObject castSpell)
    {
        if (currentAttack == null)
        {
            var copy = Instantiate(castSpell, activeFirePoint.transform.position, Quaternion.identity);
            //copy.transform.eulerAngles = rotationSetting;
            currentAttack = copy;
            if (copy.GetComponent<scr_spells>().PushSpell)
            {
                Destroy(copy, dTime);
            }
            
        }
    }

    void Summon(GameObject familiar) //We need a way to keep track of how many summons we have, and to know when they die. Check each familiar slot and proceed if empty?
                                     //How do we decide which to assign the spawn to?
    {
        if (!spellOnCooldown) 
        {
            if (!CheckForOpenFamiliarSlots()) { DestroyFamiliar(); }
            var copy = Instantiate(familiar, activeFirePoint.transform.position, Quaternion.identity);
            if (copy.GetComponent<enemyAI_Script>() != null) 
            {
                Debug.Log("Summoned enemy");
                copy.GetComponent<enemyAI_Script>().whatIsPlayer = LayerMask.GetMask("Enemies");
                copy.GetComponent<enemyAI_Script>().bodyguard = true;
                copy.GetComponent<enemyAI_Script>().ward = this.gameObject;
                copy.layer = gameObject.layer;
            }
            SlotMyFamiliar(copy);
            spellOnCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }

    void ResetCooldown()
    {
        spellOnCooldown = false;
    }

    void Dash()
    {
        dashing = true;
        Debug.Log("Dash");
        //body.MovePosition(body.position + velocity * dashSpd * Time.deltaTime);
        //body.MovePosition(body.position);

        var direction = activeFirePoint.transform.position - body.transform.position;
        body.AddForce(direction.normalized * dashSpd, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashCooldown);
        //body.velocity = Vector3.zero;
        //body.angularVelocity = Vector3.zero;

        //body.MovePosition(activeFirePoint.transform.position );
    }

    private void ResetDash()
    {
        body.constraints = RigidbodyConstraints.FreezePosition;
        body.constraints = RigidbodyConstraints.None;
        body.constraints = RigidbodyConstraints.FreezeRotationX;
        body.constraints = RigidbodyConstraints.FreezeRotationZ;
        dashing = false;
    }

    void Death()
    {
        Debug.Log("YOU DIED");
        // Code for death, reset scene?
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + velocity * mSpd * Time.fixedDeltaTime);
        transform.eulerAngles = rotationSetting;

    }
}

