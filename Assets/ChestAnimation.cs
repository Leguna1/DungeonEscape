using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private GameObject openChest;
    [SerializeField] private GameObject closeChest;

    private void Start()
    {
        // Ensure these are assigned in the Inspector
        if (openChest == null || closeChest == null)
        {
            Debug.LogError("Open and Close Chest references are not set!");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Only proceed if both chest references are assigned
            if (closeChest != null && openChest != null)
            {
                closeChest.SetActive(false);
                openChest.SetActive(true);
            }
        }
    }
}