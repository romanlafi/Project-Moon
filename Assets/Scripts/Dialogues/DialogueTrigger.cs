using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialog dialog;
    public GameObject interactionButton;

    public void TriggerDialogue ()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialog);
    }

    public void NextDialogue ()
    {
        FindAnyObjectByType<DialogueManager>().DisplayNextSentence();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
            FindAnyObjectByType<DialogueManager>().animator.SetBool("isOpen", false);
        }
    }
}
