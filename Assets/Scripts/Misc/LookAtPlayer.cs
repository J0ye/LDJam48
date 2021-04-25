using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public string lookFor = "Player";

    protected GameObject player;

    // Start is called before the first frame update
    protected void Awake()
    {
        if (player == null && lookFor != "")
        {
            player = GameObject.FindGameObjectWithTag(lookFor);
        }
    }

    // Update is called once per frame
    protected void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("There is no " + lookFor + " object in the scene. Help me here: " + gameObject.name);
            if(GameObject.FindGameObjectWithTag(lookFor) != null)
            {
                player = GameObject.FindGameObjectWithTag(lookFor);
            }
        }

        if(player != null)
        {
            transform.LookAt(player.transform.position);
        }
    }
}
