using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject interactionButton;
    public TextMeshProUGUI dialogText;
    public string dialog;

    private bool playerInRange;

    private PlayerInputActions playerControls;
    private InputAction interact;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionButton.SetActive(true);
        }    
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
            interactionButton.SetActive(false);
        }    
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(playerInRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                interactionButton.SetActive(true);
            }
            else
            {
                interactionButton.SetActive(false);

                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
}
