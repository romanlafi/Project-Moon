using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;

    private AudioSource sentencesAudioSource;
    public TMP_Text dialogText;
    public TMP_Text nameText;

    private Queue<string> sentencesText;
    private Queue<AudioClip> sentencesAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        sentencesText = new Queue<string>();
        sentencesAudio = new Queue<AudioClip>();
    }

    public void StartDialogue (Dialog dialog)
    {
        nameText.text = dialog.name;

        sentencesText.Clear();
        sentencesAudio.Clear();

        for (int i=0 ; i<dialog.sentencesText.Length; i++)
        {
            sentencesText.Enqueue(dialog.sentencesText[i]);
            sentencesAudio.Enqueue(dialog.sentencesAudio[i]);
        }

        sentencesAudioSource = GameObject.Find(dialog.name).GetComponent<AudioSource>();

        animator.SetBool("isOpen", true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence () 
    {
        if (sentencesText.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentencesText.Dequeue();
        sentencesAudioSource.clip = sentencesAudio.Dequeue();

        sentencesAudioSource.Play();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = string.Empty;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue () 
    {
        animator.SetBool("isOpen", false);
        sentencesAudioSource.Stop();
    }
}
