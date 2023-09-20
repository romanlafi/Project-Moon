using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    idle,
    walk,
    interact,
    attack,
    stagger
}

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private PlayerInputActions playerControls;
    private InputAction move;
    private InputAction attack;
    private InputAction interact;

    private DialogueTrigger dialog;
    private DialogueManager dialogueManager;

    private Sign sign;
    private LampBehavior lamp;

    private Transform otherTransform;

    private CharacterStats stats;

    public PlayerState currentState;

    public CharacterSounds characterSounds;

    private void Awake () 
    {
        playerControls = new PlayerInputActions();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterSounds = GetComponent<CharacterSounds>();

        dialogueManager = FindObjectOfType<DialogueManager>();
        currentState = PlayerState.walk;
    }

    private void OnEnable () 
    {
        move = playerControls.Player.Move;
        move.Enable();

        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable () 
    {
        move.Disable();
        attack.Disable();
        interact.Disable();
    }

    void Update ()
    {
        if (currentState != PlayerState.stagger) 
        {
            Move();
        }

        if (otherTransform != null)
        {
            AdjustOrderLayer();
        }
    }

    public void ChangeState (PlayerState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void Move ()
    {
        moveDirection = move.ReadValue<Vector2>();
        moveDirection.Normalize();

        rigidBody.velocity = new Vector2(moveDirection.x * stats.moveSpeed.GetValue(), moveDirection.y * stats.moveSpeed.GetValue());

        if (moveDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);
            animator.SetBool("moving", true);

            characterSounds.Walk();
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void Interact (InputAction.CallbackContext context)
    {
        if (sign != null)
        {
            sign.ShowDialog();
        }

        if (lamp != null)
        {
            lamp.Rest();
        }

        if (dialog != null)
        {
            if (dialogueManager.animator.GetBool("isOpen"))
            {
                dialog.NextDialogue();
            }
            else
            {
                dialog.TriggerDialogue();
            }
        }
    }

    private void Attack (InputAction.CallbackContext context)
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        } 
    }

    private IEnumerator AttackCo ()
    {
        ChangeState(PlayerState.attack);
        animator.SetBool("attacking", true);
        characterSounds.Attack();
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        ChangeState(PlayerState.walk);
    }

    public void Knock (float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }

    private IEnumerator KnockCo (float knockTime)
    {
        if (rigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidBody.velocity = Vector2.zero;
            ChangeState(PlayerState.walk);
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void AdjustOrderLayer ()
    {
        if (transform.position.y > otherTransform.position.y)
        {
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            spriteRenderer.sortingOrder = 2;
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        otherTransform = other.transform;
        Debug.Log(otherTransform);

        if (other.gameObject.CompareTag("Sign"))
        {
            sign = other.gameObject.GetComponent<Sign>();
        }

        if (other.gameObject.CompareTag("Lamp"))
        {
            lamp = other.gameObject.GetComponent<LampBehavior>();
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            dialog = other.gameObject.GetComponent<DialogueTrigger>();
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        otherTransform = null;

        if (other.gameObject.CompareTag("Sign"))
        {
            sign = null;
        }

        if (other.gameObject.CompareTag("Lamp"))
        {
            lamp = null;
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            dialog = null;
        }
    }
}


