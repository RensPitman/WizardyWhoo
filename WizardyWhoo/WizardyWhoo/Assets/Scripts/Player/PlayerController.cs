using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterJoint MyJoint;
    private Rigidbody currentRigidbody;

    public LayerMask ObjectMask;
    public LayerMask HoldObjectMask;

    private bool hasHoldObject;

    public float FarPlane = 1;
    private Vector3 mousePos;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, FarPlane));

        if(Input.GetMouseButtonDown(0))
        {
            if(!hasHoldObject)
            {
                hasHoldObject = true;
                CheckForObject();
            }
            else
            {
                hasHoldObject = false;
                DetachObject();
            }
        }


        transform.position = Vector3.MoveTowards(transform.position, mousePos, 0.5f);
    }

    void CheckForObject()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, ObjectMask))
        {
            AttachObject(hit.collider.gameObject.GetComponentInParent<Rigidbody>());
        }
    }

    void AttachObject(Rigidbody rigid)
    {
        currentRigidbody = rigid;
        MyJoint.connectedBody = currentRigidbody;

        currentRigidbody.drag = 3;

        currentRigidbody.transform.GetChild(0).gameObject.layer = Mathf.RoundToInt(Mathf.Log(HoldObjectMask.value, 2));
    }

    void DetachObject()
    {
        currentRigidbody.transform.GetChild(0).gameObject.layer = Mathf.RoundToInt(Mathf.Log(ObjectMask.value, 2));

        currentRigidbody.drag = 0;

        MyJoint.connectedBody = null;
        currentRigidbody = null;
    }

}
