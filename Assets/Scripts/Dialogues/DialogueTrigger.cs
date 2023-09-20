using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    public Dialog dialog;
    public GameObject interactionButton;
    public AudioSource audio;

    public float fadeTime;

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
            StartCoroutine(FadeOutAudio(audio, fadeTime));
            FindAnyObjectByType<DialogueManager>().animator.SetBool("isOpen", false);
        }
    }

    public static IEnumerator FadeOutAudio(AudioSource audio, float fadeTime)
    {
        float startVolume = audio.volume;

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;
    }
}
