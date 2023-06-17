using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehavior : MonoBehaviour
{
    public GameObject interactionButton;
    public LevelLoader levelLoader;

    public void Rest ()
    {
        levelLoader.RefreshLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionButton != null)
            {
                interactionButton.SetActive(false);
            }
        }
    }
}
