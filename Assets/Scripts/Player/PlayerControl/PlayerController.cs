using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    walk,
    interact,
    attack
}

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    private Animator animator;
    private Rigidbody2D rigidBody;
    
    private PlayerInputActions playerControls;
    private InputAction move;
    private InputAction attack;

    private CharacterStats stats;

    private void Awake() 
    {
        playerControls = new PlayerInputActions();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
    }

    private void OnEnable() 
    {
        move = playerControls.Player.Move;
        move.Enable();

        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;
    }

    private void OnDisable() 
    {
        move.Disable();
        attack.Disable();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = move.ReadValue<Vector2>();
        moveDirection.Normalize();

        rigidBody.velocity = new Vector2(moveDirection.x * stats.moveSpeed.GetValue(), moveDirection.y * stats.moveSpeed.GetValue());

        if (moveDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void Attack (InputAction.CallbackContext context)
    { 
        StartCoroutine(AttackCo());   
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);   
    }
}
