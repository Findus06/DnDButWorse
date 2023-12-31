using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] float attackRange = 1f;
    Animator animator;
    CharacterMovement characterMovement;

    InteractableObject target;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }
    internal void Attack(InteractableObject target)
    {
        this.target = target;
        ProcessAttack();
    }

    void Update()
    {
        if(target != null)
        {
            ProcessAttack();
        }
        
    }

    private void ProcessAttack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < attackRange)
        {
            characterMovement.Stop();
            animator.SetTrigger("Attack");
            target = null;
        }
        else
        {
            characterMovement.SetDestination(target.transform.position);
        }
    }
}
