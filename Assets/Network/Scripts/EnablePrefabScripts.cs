using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePrefabScripts : MonoBehaviour
{
    public List<MonoBehaviour> Scripts;
    // Start is called before the first frame update

    public void EnableScripts()
    {
        foreach(var script in Scripts)
        {
            script.enabled = true;
        }
    }

}
