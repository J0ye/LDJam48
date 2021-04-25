using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Hacky
        Text scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        scoreDisplay.text = PlayerPrefs.GetInt("Score").ToString();
    } 
}
