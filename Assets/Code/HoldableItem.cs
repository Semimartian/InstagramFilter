using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoldableItem : MonoBehaviour
{
    public Rigidbody rigidbody;

    private void Start()
    {
        if(rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
            
        }
    }

}
