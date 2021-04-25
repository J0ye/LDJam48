using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayScore : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = Mathf.RoundToInt(GameManager._instance.depth).ToString() + " m"; 
    }
}
