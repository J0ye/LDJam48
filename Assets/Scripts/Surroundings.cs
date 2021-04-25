using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surroundings : MonoBehaviour
{
    public List<GameObject> entitys = new List<GameObject>();

    private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position - DirectionToNearestObject());
    }

    public Vector3 DirectionToNearestObject()
    {
        Vector3 dir = Vector3.zero;
        foreach(GameObject go in entitys)
        {
            Vector3 toObject = transform.position - go.transform.position;
            if (dir == Vector3.zero)
            {
                dir = toObject;
            } else if(toObject.magnitude < dir.magnitude)
            {
                dir = toObject;
            }
        }
        return dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            entitys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (entitys.Contains(other.gameObject))
        {
            entitys.Remove(other.gameObject);            
        }
    }
}
