using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    [Tooltip("Add a mono behaviour script to the list that you wish to manage. The script will be deactivated if its active on execution or vice versa")]
    public List<MonoBehaviour> components = new List<MonoBehaviour>();

    public void Execute()
    {
        foreach (MonoBehaviour script in components)
        {
            script.enabled = !script.enabled;
        }
    }
}
