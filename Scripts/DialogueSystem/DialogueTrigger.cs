using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas; // Reference to the dialogue canvas

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the trigger collider
        if (other.CompareTag("Player"))
        {
            // Enable the dialogue canvas
            dialogueCanvas.SetActive(true);
            // Optionally, you can also disable the trigger if you want it to be one-time use
            // GetComponent<Collider2D>().enabled = false; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger collider
        if (other.CompareTag("Player"))
        {
            // Disable the dialogue canvas
            dialogueCanvas.SetActive(false);
        }
    }
}

