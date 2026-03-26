using UnityEngine;

public class PickupTrash : MonoBehaviour
{
    public float pickupRange = 2f;              
    public Transform holdPoint;                 

    private TrashEnum carriedTrash = null;     

    void Update()
    {
        if (carriedTrash == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
                Pickup();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
                Drop();
        }
    }

    void Pickup()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickupRange))
        {
            TrashEnum trash = hit.collider.GetComponent<TrashEnum>();

            if (trash != null)
            {
                carriedTrash = trash;

                Rigidbody rb = trash.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                trash.transform.SetParent(holdPoint);
                trash.transform.localPosition = Vector3.zero;
                trash.transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void Drop()
    {
        carriedTrash.transform.SetParent(null);
        Rigidbody rb = carriedTrash.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;
        carriedTrash = null;
    }

    public TrashEnum GetCarriedTrash()
    {
        return carriedTrash;
    }
}

