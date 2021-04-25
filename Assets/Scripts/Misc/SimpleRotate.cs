using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Vector3 rot = Vector3.zero;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rot * speed * Time.deltaTime, Space.World);
    }
}
