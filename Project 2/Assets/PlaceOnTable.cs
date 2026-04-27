using UnityEngine;

public class PlaceOnTable : MonoBehaviour
{
    public void PlaceOnSurface()
    {
        RaycastHit hit;

        // Cast downward from object
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.CompareTag("Table"))
            {
                // Place object exactly on surface
                float objectHeight = GetComponent<Collider>().bounds.extents.y;

                transform.position = hit.point + new Vector3(0, objectHeight, 0);
            }
        }
    }
}
