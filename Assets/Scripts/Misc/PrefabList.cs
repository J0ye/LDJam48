using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabList : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject end;

    public void SpawnRandom()
    {
        Instantiate(GetRandom());
    }

    public GameObject GetRandom()
    {        
        int rand = UnityEngine.Random.Range(0, prefabs.Count);

        return prefabs[rand];
    }

    public GameObject GetExclusive()
    {
        if(prefabs.Count > 0)
        {            
            int rand = UnityEngine.Random.Range(0, prefabs.Count);
            GameObject target = prefabs[rand];
            prefabs.RemoveAt(rand);
            return target;
        } else {
            return end;
        }
    }
}
