using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronBehaviour : MonoBehaviour
{
    [Header("Object Check")]
    public Vector3 Size;
    public Vector3 Offset;
    public LayerMask ObjectMask;

    private void Update()
    {
        CheckForObjects();
    }

    void CheckForObjects()
    {
        Collider[] coll = Physics.OverlapBox(transform.position + Offset, Size, Quaternion.identity, ObjectMask);

        if(coll.Length > 0)
        {
            print("Put in the mix");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Offset, Size);
    }
}
