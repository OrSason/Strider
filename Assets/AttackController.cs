using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Animator characterAnimator;
    bool attackingStatus =false;
    public float attackDuration=10f;


    void Awake()
    {
        characterAnimator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            setAttacking(attackDuration);
        } 
    }


    public void setAttackingStatus()
    {
        characterAnimator.SetBool("isAttacking", true);
    }

    public void setIdleStatus()
    {
        characterAnimator.SetBool("isAttacking", false);
    }


    public void setAttacking(float attackDuration)
    {
        setAttackingStatus();
        Invoke("setIdleStatus", attackDuration);
    }
}
