using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private Transform attack2HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetails = new float[2];

    private Animator anim;

    private PlayerMovement PC;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {        
        CheckAttacks();
    }

    private void CheckAttacks()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {                
                lastInputTime = Time.time;

                //Perform Attack1
                if (!isAttacking)
                {
                    gotInput = false;
                    isAttacking = true;
                    isFirstAttack = !isFirstAttack;
                    anim.SetBool("attack1", true);
                    anim.SetBool("firstAttack", isFirstAttack);
                    anim.SetBool("isAttacking", isAttacking);
                }




            }
        }




        if (Time.time >= lastInputTime + inputTimer)
        {
            //Wait for new input
            gotInput = false;
        }
    }














}
