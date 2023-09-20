using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip walkingSound;
    public AudioClip attackSound;

    public void Walk()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
    }

    public void Attack()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }
}
