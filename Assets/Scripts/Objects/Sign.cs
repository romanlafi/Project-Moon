using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject interactionButton;
    public TextMeshProUGUI dialogText;
    public string dialog;

    public void ShowDialog()
    {
        if (dialogBox.activeInHierarchy)
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

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
        }    
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
            interactionButton.SetActive(false);
        }    
    }
}
