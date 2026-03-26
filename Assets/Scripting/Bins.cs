using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType acceptedType;   // Set in Inspector

    private bool playerInRange = false;
    private PickupTrash player;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            player = other.GetComponentInParent<PickupTrash>();
            playerInRange = true;
            Debug.Log("Press F to throw trash");
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }



void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            TryThrowTrash();
        }
    }

    void TryThrowTrash()
    {
        TrashEnum carried = player.GetCarriedTrash();

        if (carried == null)
        {
            Debug.Log("You are not holding anything");
            return;
        }

        if (carried.type == acceptedType)
        {
            Debug.Log("Correct bin!");
            Destroy(carried.gameObject);
            player.Drop();
        }
        else
        {
            Debug.Log("Wrong bin!");
        }
    }
}

