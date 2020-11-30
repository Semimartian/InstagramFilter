using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private HoldableItem myHeldItem = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(myHeldItem == null)
            {
                Vector2 mousePosition = Input.mousePosition;
                Ray ray = camera.ScreenPointToRay(mousePosition);
                RaycastHit raycastHit;
                Physics.Raycast(ray, out raycastHit);
                if (raycastHit.collider != null)
                {
                    Debug.Log(raycastHit.collider.gameObject.name);
                    HoldableItem item = (raycastHit.collider.gameObject.GetComponentInParent<HoldableItem>());
                   /* if (item == null)
                    {
                        item = (raycastHit.collider.gameObject.GetComponentInParent<HoldableItem>());
                    }*/
                    if (item != null)
                    {
                        GrabItem(item);
                    }
                }
            }
    
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseItem();
        }
    }

    /* private Vector3 GetMouseWorldPosition()
     {
         Vector3 mousePosition = Input.mousePosition;
         // mousePosition.z = 1;// camera.transform.position.y;
         mousePosition = MouseToGroundPlane(mousePosition);
        // mousePosition = camera.ScreenToWorldPoint(mousePosition);
         mousePosition.y = itemHeightWhileBeingHeld;
         return mousePosition;
     }*/


    /*[SerializeField] private float groundY = 1;
    [SerializeField] private Transform mouseRayMarker;
    private Vector3 MouseToGroundPlane(Vector3 mousePosition)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        float rayLength = (ray.origin.y - groundY) / ray.direction.y;

        Debug.DrawLine(ray.origin, ray.origin-( ray.direction* rayLength), Color.red);

        Vector3 results = ray.origin - (ray.direction * rayLength);
        mouseRayMarker.position = results;
        return ray.origin - (ray.direction * rayLength);
    }*/

    //[SerializeField] float MouseZ;
    private Vector3 previousMousePosition;
    private void FixedUpdate()
    {
        //NOTE = Might break if camera's Z is not 0......
        if (myHeldItem != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = myHeldItem.rigidbody.position.z;
            Vector3 newPosition = camera.ScreenToWorldPoint(mousePosition);
            Vector3 difference = newPosition-previousMousePosition;
            difference.z = 0;
            //myHeldItem.rigidbody.position = newPosition;
            myHeldItem.rigidbody.position += difference;
            previousMousePosition = newPosition;

        }

    }

    private void GrabItem(HoldableItem item)
    {
        myHeldItem = item;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = myHeldItem.rigidbody.position.z;
        previousMousePosition = camera.ScreenToWorldPoint(mousePosition);

    }

    private void ReleaseItem()
    {
        myHeldItem = null;
    }


}
