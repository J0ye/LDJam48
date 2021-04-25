using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float lenght = 6f;
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraTransform.position.y < transform.position.y-lenght)
        {
            transform.position = new Vector3(0, cameraTransform.position.y-lenght, 0f);
        }
    }
}
