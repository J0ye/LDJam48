using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundloop : MonoBehaviour
{
    public List<GameObject> elements = new List<GameObject>();
    public float length = 10f;

    private GameObject target;
    private float border;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = elements.Count - 1;
        target = elements[i];
        border = elements[0].transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.transform.position.y < border)
        {
            ResetElement();
        }
    }

    private void ResetElement()
    {
        float newY = target.transform.position.y - length;
        target.transform.position = new Vector3(target.transform.position.x, newY, target.transform.position.z);
        border = target.transform.position.y;
        i--;
        if (i < 0) i = elements.Count - 1;
        target = elements[i];
    }
}
