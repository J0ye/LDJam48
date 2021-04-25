using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LookAtTarget : LookAtPlayer
{
    public GameObject target;

    private Vector3 startPos;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();

        if(target != null)
        {
            player = target;
        }

        startPos = transform.position;
    }

    // Update is called once per frame
    new void LateUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if(hit.transform.gameObject != player)
            {
                Debug.Log("Hol Up. Who dat.");
            }else if(transform.position != startPos) 
            {
            }
        }

        base.LateUpdate();
    }
}
