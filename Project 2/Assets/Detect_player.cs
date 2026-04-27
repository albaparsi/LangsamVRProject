using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string message = "Wassup Leo!";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(message);
        }
    }
}