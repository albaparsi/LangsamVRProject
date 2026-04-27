using UnityEngine;

public class IgnorePlayerColliders : MonoBehaviour
{
    void Start()
    {
        var myCol = GetComponent<Collider>();
        if (myCol == null) return;

        var rig = GameObject.Find("[BuildingBlock] Camera Rig");
        if (rig == null) return;

        foreach (var col in rig.GetComponentsInChildren<Collider>(true))
        {
            Physics.IgnoreCollision(myCol, col, true);
        }
    }
}