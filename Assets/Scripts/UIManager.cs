using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public List<GameObject> lifes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        if (lifes.Count <= 0) return;
        int i = lifes.Count - 1;
        Destroy(lifes[i]);
        lifes.RemoveAt(i);
    }
}
