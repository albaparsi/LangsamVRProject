using UnityEngine;

public class Wolf_Interact_with_Player : MonoBehaviour
{
    public string message = "Hey there!";
    public AudioSource audioSource;
    public GameObject textUI; // optional

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            Debug.Log(message);

            if (audioSource != null)
                audioSource.Play();

            if (textUI != null)
                textUI.SetActive(true);
        }
    }
}