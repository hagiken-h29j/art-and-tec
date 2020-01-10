using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRangeMode(int num, string type)
    {
        opt[type] = num;
        Debug.Log("Set : (" + type + ", " + opt[type] + ")");
    }
    
    protected static Dictionary<string, int> opt = new Dictionary<string, int>()
    {
        {"range", 0},//0:水平360°,1:全方位
        {"level", 1},
        {"assistSE", 1}
    };

    public static Dictionary<string, int> GetOptData()
    {
        return opt;
    }
}
