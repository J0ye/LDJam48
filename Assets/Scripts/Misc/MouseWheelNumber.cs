using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWheelNumber : MonoBehaviour
{
    public Vector2 wheelState;
    public float number;
    [Range(0.01f, 30f)]
    public float scale = 1f;


    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta != Vector2.zero)
        {
            wheelState += Input.mouseScrollDelta * scale;
            number = wheelState.y;
        }
    }

    public int GetInt()
    {
        return (int)number;
    }

    public float GetFloat()
    {
        return number;
    }
}
