using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public List<GameObject> lifes = new List<GameObject>();
    public Text depthDisplay;

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

    public void Hit()
    {
        if (lifes.Count <= 0) return;
        int i = lifes.Count - 1;
        Destroy(lifes[i]);
        lifes.RemoveAt(i);
    }
}
