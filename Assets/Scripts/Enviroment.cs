using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float lenght = 6f;
    public float effect = 1f;
    private Transform cameraTransform;
    private float height, startpos;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startpos = transform.position.y;
        height = transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if(cameraTransform.position.y < transform.position.y-lenght)
        {
            transform.position = new Vector3(0, cameraTransform.position.y-lenght, 0f);
        }
        float temp = Camera.main.transform.position.y * (1 - effect);
        float dist = Camera.main.transform.position.y * effect;

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if (temp > startpos + height) startpos += height;
        else if (temp < startpos - height) startpos -= lenght;*/
    }
}
